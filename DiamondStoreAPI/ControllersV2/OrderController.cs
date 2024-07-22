using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Services.Implement;
using Services.DTOs.Request;
using Microsoft.Identity.Client;
using Services.DTOs.Response;
using Humanizer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.X509;

namespace DiamondStoreAPI.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/Order/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService iOrderService;
        private readonly IOrderDetailService iOrderDetailService;
        private readonly IProductService iProductService;
        private readonly ICustomerService iCustomerService;
        private readonly IPaymentService iPaymentService;
        private readonly IProductMaterialService iProductMaterialService;
        private readonly IMaterialCategoryService iMaterialCategoryService;
        private readonly IRefundService iRefundService;
        public OrdersController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService, ICustomerService customerService, IPaymentService paymentService, IProductMaterialService productMaterialService, IMaterialCategoryService materialCategoryService, IRefundService refundService)
        {
            iOrderService = orderService;
            iOrderDetailService = orderDetailService;
            iProductService = productService;
            iCustomerService = customerService;
            iPaymentService = paymentService;
            iProductMaterialService = productMaterialService;
            iMaterialCategoryService = materialCategoryService;
            iRefundService = refundService;
        }

        [Authorize(Roles = "SaleStaff, Shipper")]
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> AcceptOrder([FromBody] OrderStatusRequest request)
        {
            var result = await iOrderService.UpdateOrderStatus(request);
            if (result)
            {
                if (request.ButtonValue == "CANCEL")
                {
                    await iProductService.UpdateProductStatusByCancelOrder((int)request.OrderID);
                    var paymentToRefund = await iPaymentService.GetPaymentByOrderId((int)request.OrderID);
                    
                    if (paymentToRefund != null)
                    {
                        var order = iOrderService.getOrderByOrderID((int)request.OrderID);
                        var refundRequest = new TblRefund()
                        {
                            PaymentId = paymentToRefund.Id,
                            RefundAmount = (order.PaymentMethod == "Received") ? (decimal)paymentToRefund.Deposits : paymentToRefund.Amount,
                            RefundStatus = "Pending",
                            Reason = "Staff cancel order"
                        };
                        await iRefundService.MakeRefund(refundRequest);
                    }
                }
                return Ok(new { Message = "Order status updated successfully." });
            }
            return BadRequest(new { Message = "Failed to update order status." });
        }

        //GET: api/Order
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<TblOrder>>> GetOrders()
        {
            return await iOrderService.GetOrders();
        }

        //GET: api/Order/5
        [Authorize]
        [HttpGet("getOrderInfo")]
        public async Task<ActionResult<TblOrder>> GetTblOrder(int id)
        {
            var orderInfo = iOrderService.GetOrderInfo(id);

            if (orderInfo == null)
            {
                return NotFound();
            }
            var payment = iPaymentService.GetPaymentByOrderId(id);
            orderInfo.TransactionID = payment.Result.TransactionId;
            orderInfo.PayerEmail = payment.Result.PayerEmail;
            orderInfo.PaymentStatus = payment.Result.PaymentStatus;
            return Ok(orderInfo);
        }


        // POST: api/Order
        [Authorize(Roles = "Customer")]
        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] NewOrderRequest newOrderRequest)
        {

            if (newOrderRequest == null)
            {
                return BadRequest();
            }

            var customerIDToOrder = iCustomerService.GetCustomerByAccount(newOrderRequest.Username).CustomerId;

            TblOrder newOrder = new TblOrder()
            {
                CustomerId = customerIDToOrder,
                PaymentMethod = newOrderRequest.PaymentMethod,
                OrderDate = newOrderRequest.OrderDate,
                OrderStatus = newOrderRequest.PaymentMethod.Equals("Received") ? "Processing" : "Accepted",
            };
            var order = iOrderService.AddOrder(newOrder);
            //add orderdetail
            TblCustomer customer = iCustomerService.GetCustomerByID((int)order.CustomerId);
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.OrderID = order.OrderId;

            foreach (var p in newOrderRequest.Products)
            {
                string productID = p.ProductID;
                int customizedSize = p.CustomizedSize;
                var product = await iProductService.GetProductAndPriceByIdAsync(productID);
                double productPrice = (double)product.ProductPrice;
                int productSize = (int)product.ProductSize;
                TblOrderDetail newOrderDetail = new TblOrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = productID,
                    CustomizedSize = customizedSize,
                    CustomizedAmount = (p.CustomizedSize == productSize) ? 0 :
                        (product.UnitSizePrice * (customizedSize - productSize)),
                    Quantity = p.Quantity,
                };
                newOrderDetail.TotalPrice = (productPrice + newOrderDetail.CustomizedAmount) * p.Quantity;

                newOrderDetail.FinalPrice = (1 - customer.DiscountRate) * newOrderDetail.TotalPrice;
                var orderDetail = iOrderDetailService.AddOrderDetail(newOrderDetail);

                //product buying for customer after charging for order
                ProductBuyingResponse productBuying = new ProductBuyingResponse()
                {
                    ProductID = product.ProductId,
                    ProductName = product.ProductName,
                    Material = iMaterialCategoryService.GetMaterialCategory(iProductMaterialService.GetProductMaterialProductID(productID).MaterialId).MaterialName,
                    Image = product.Image,
                    CustomizedSize = p.CustomizedSize,
                    Quantity = p.Quantity,
                    Price = (decimal)orderDetail.TotalPrice
                };
                orderInfo.products.Add(productBuying);
                orderInfo.TotalPrice = orderInfo.TotalPrice += (decimal)orderDetail.TotalPrice;

                orderInfo.FinalPrice = orderInfo.FinalPrice += (decimal)orderDetail.FinalPrice;
            }
            orderInfo.DiscountRate = (decimal)customer.DiscountRate;
            orderInfo.OrderDate = newOrderRequest.OrderDate;
            orderInfo.OrderStatus = order.OrderStatus;
            orderInfo.CustomerID = customer.CustomerId;
            TblPayment newPayMent = new TblPayment()
            {
                OrderId = order.OrderId,
                CustomerId = customer.CustomerId,
                PaymentMethod = order.PaymentMethod,
                Deposits = newOrderRequest.Deposits,
                TransactionId = newOrderRequest.TransactionId,
                PayerEmail = newOrderRequest.PayerEmail,
                Amount = (decimal)orderInfo.FinalPrice,
                Currency = "USD",
                PaymentStatus = newOrderRequest.PaymentStatus,
                PaymentDate = newOrderRequest.OrderDate
            };
            newPayMent.PayDetail = newPayMent.PaymentMethod + "-Deposits: " + newPayMent.Deposits;
            var payment = iPaymentService.AddPayment(newPayMent);
            orderInfo.Deposits = (double)payment.Deposits;
            return Ok(orderInfo);
        }

        [Authorize]
        [HttpGet("OrderHistory")]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetOrderHistory(string username)
        {
            var orderHistory = iOrderService.GetOrderHistory(username);
            return Ok(orderHistory);
        }

        [HttpPut("CancelOrder")]
        [Authorize]
        public async Task<IActionResult> CancelOrder(int orderID)
        {
            var orderToUpdate = iOrderService.getOrderByOrderID(orderID);
            if (orderToUpdate == null)
            {
                return NotFound();
            }

            if (orderToUpdate.OrderStatus.Equals("Deliverying") || orderToUpdate.OrderStatus.Equals("Deliveried") || orderToUpdate.Equals("Cancelled"))
            {
                return BadRequest("Cannot cancel");
            }
            else
            {
                orderToUpdate.OrderStatus = "Cancelled";
                orderToUpdate.OrderNote = "Customer cancelled";
                await iOrderService.UpdateOrder(orderToUpdate);

                //var productsBuying = iOrderService.GetOrderInfo(orderID).products;
                //foreach (var p in productsBuying)
                //{
                //    var product = await iProductService.GetProductByIdAsync(p.ProductID);
                //    product.Status = true;
                //    await iProductService.UpdateProductAsync(product.ProductId, product);
                //}
                await iProductService.UpdateProductStatusByCancelOrder(orderID);

                var paymentToRefund = await iPaymentService.GetPaymentByOrderId(orderID);
                if (paymentToRefund != null)
                {
                    var refundRequest = new TblRefund()
                    {
                        PaymentId = paymentToRefund.Id,
                        RefundAmount = (orderToUpdate.PaymentMethod == "Received") ? (decimal)paymentToRefund.Deposits : paymentToRefund.Amount,
                        RefundStatus = "Pending",
                        Reason = "Customer cancel order"
                    };
                    await iRefundService.MakeRefund(refundRequest);
                }

                return Ok("Cancel successfully");
            }
        }

        [Authorize(Roles = "SaleStaff, Shipper, Manager")]
        [HttpGet("GetOrderInfoListForSaleStaff")]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetOrderInfoListForSaleStaff()
        {
            var orderInfoList = await iOrderService.GetOrderInfoListForSaleStaff();
            if (orderInfoList.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(orderInfoList);
        }

        [Authorize(Roles = "SaleStaff, Shipper, Manager")]
        [HttpGet("GetOrderInforListForShipper")]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetOrderInforListForShipper()
        {
            var acceptedOrderInfoList = await iOrderService.GetOrderInforListForShipper();
            if (acceptedOrderInfoList.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(acceptedOrderInfoList);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("OrderCount")]
        public async Task<IActionResult> OrderCount()
        {
            var result = await iOrderService.GetOrderStatusCountAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetRevenue")]
        public async Task<IActionResult> GetRevenue()
        {
            var result = await iOrderService.GetTotalRevenueAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetRevenuePerMonthOfYear")]
        public async Task<IActionResult> GetRevenuePerMonthOfYear(int year)
        {
            var result = await iOrderService.GetRevenuePerMonthOfYear(year);
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetNumberDeliveriedOrderPerMonthOfYear")]
        public async Task<IActionResult> GetNumberDeliveriedOrderPerMonthOfYear(int year)
        {
            var result = await iOrderService.GetNumberOrdersPerMonthOfYear(year);
            return Ok(result);
        }
    }
}
