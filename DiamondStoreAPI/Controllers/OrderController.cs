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

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService iOrderService;
        private readonly IOrderDetailService iOrderDetailService;
        private readonly IProductService iProductService;
        private readonly ICustomerService iCustomerService;
        private readonly IPaymentService iPaymentService;
        private readonly IProductMaterialService iProductMaterialService;
        private readonly IMaterialCategoryService iMaterialCategoryService;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService, ICustomerService customerService, IPaymentService paymentService, IProductMaterialService productMaterialService, IMaterialCategoryService materialCategoryService)
        {
            iOrderService = orderService;
            iOrderDetailService = orderDetailService;
            iProductService = productService;
            iCustomerService = customerService;
            iPaymentService = paymentService;
            iProductMaterialService = productMaterialService;
            iMaterialCategoryService = materialCategoryService;
        }

        [HttpPut("UpdateOrderStatus")]

        public async Task<IActionResult> AcceptOrder([FromBody] OrderStatusRequest request)
        {
            var result = await iOrderService.UpdateOrderStatus(request);
            if (result)
            {
                return Ok(new { Message = "Order status updated successfully." });
            }
            return BadRequest(new { Message = "Failed to update order status." });
        }

        //GET: api/Order
        [HttpGet]
        public async Task<ActionResult<List<TblOrder>>> GetOrders()
        {
            return await iOrderService.GetOrders();
        }

        //GET: api/Order/5
        [HttpGet("getOrderInfo")]
        public async Task<ActionResult<TblOrder>> GetTblOrder(int id)
        {
            var orderInfo = iOrderService.GetOrderInfo(id);

            if (orderInfo == null)
            {
                return NotFound();
            }

            return Ok(orderInfo);
        }


        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                    Price = (double)orderDetail.TotalPrice
                };
                orderInfo.products.Add(productBuying);
                orderInfo.TotalPrice = orderInfo.TotalPrice += (double)orderDetail.TotalPrice;

                orderInfo.FinalPrice = orderInfo.FinalPrice += (double)orderDetail.FinalPrice;
            }
            orderInfo.DiscountRate = (double)customer.DiscountRate;
            orderInfo.OrderDate = newOrderRequest.OrderDate;
            orderInfo.OrderStatus = order.OrderStatus;
            orderInfo.CustomerID = customer.CustomerId;
            TblPayment newPayMent = new TblPayment()
            {
                OrderId = order.OrderId,
                CustomerId = customer.CustomerId,
                PaymentMethod = order.PaymentMethod,
                Deposits = newOrderRequest.Deposits,
            };
            newPayMent.PayDetail = newPayMent.PaymentMethod + "-Deposits: " + newPayMent.Deposits;
            var payment = iPaymentService.AddPayment(newPayMent);
            orderInfo.Deposits = (double)payment.Deposits;
            return Ok(orderInfo);
        }

        [HttpGet("OrderHistory")]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetOrderHistory(string username)
        {
            var orderHistory = iOrderService.GetOrderHistory(username);
            return Ok(orderHistory);
        }

        [HttpPut("CancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] CustomerCancelOrderRequest request)
        {
            var orderToUpdate = iOrderService.getOrderByOrderID(request.OrderID);
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
                orderToUpdate.OrderNote = "Customer cancelled-" + request.Note.Trim();
                var isUpdate = iOrderService.UpdateOrder(orderToUpdate);
                var productsBuying = iOrderService.GetOrderInfo(request.OrderID).products;
                foreach (var p in productsBuying) 
                {
                    iProductService.UpdateProductStatus(p.ProductID);
                }
                
                return Ok("Cancel successfully");
            }
        }

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

        [HttpGet("GetSumOrderbyMonthAndYear")]
        public async Task<IActionResult> GetSumOrderbyMonthAndYear([FromQuery] MonthYearCriteria criteria)
        {
            var list = await iOrderService.GetDeliveriedOrdersByMonthYear(criteria);
            var result = list.Count();
            return Ok(result);
        }

        [HttpGet("OrderQuantity")]
        public async Task<IActionResult> OrderQuantity()
        {
            var result = await iOrderService.GetOrderStatusCountAsync();
            return Ok(result);
        }

        [HttpGet("GetRevenue")]
        public async Task<IActionResult> GetRevenue([FromQuery] MonthYearCriteria criteria)
        {
            var result = await iOrderService.GetSumRevenue(criteria);
            return Ok(result);
        }
        
    }
}
