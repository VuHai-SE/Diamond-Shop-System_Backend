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

        //GET: api/Order
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TblOrder>>> GetTblOrders()
        //{
        //    return Ok();
        //}

        // GET: api/Order/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblOrder>> GetTblOrder(int id)
        //{
        //    var tblOrder = await iOrderService.TblOrders.FindAsync(id);

        //    if (tblOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblOrder;
        //}

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblOrder(int id, TblOrder tblOrder)
        //{
        //    if (id != tblOrder.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    iOrderService.Entry(tblOrder).State = EntityState.Modified;

        //    try
        //    {
        //        await iOrderService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblOrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] NewOrderRequest newOrderRequest)
        {

            if (newOrderRequest == null)
            {
                return BadRequest();
            }

            var customerIDToOrder = iCustomerService.GetCustomerByAccount(newOrderRequest.AccountID).CustomerId;

            TblOrder newOrder = new TblOrder()
            {
                CustomerId = customerIDToOrder,
                PaymentMethod = newOrderRequest.PaymentMethod,
                OrderDate = newOrderRequest.OrderDate,
                OrderStatus = newOrderRequest.PaymentMethod.Equals("Received") ? "Processing" : "Accepted",
                ShipStatus = "Pending",
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
                TblProduct product = iProductService.GetProduct(productID);
                double productPrice = (double)((product.GemCost + product.MaterialCost + product.ProductionCost) * (1 + product.PriceRate/100));
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
                    ProductCode = product.ProductCode,
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
            //add new Payment
            TblPayment newPayMent = new TblPayment()
            {
                OrderId = order.OrderId,
                CustomerId = customer.CustomerId,
                PaymentMethod = order.PaymentMethod,
                Deposits = newOrderRequest.Deposits,
            };
            newPayMent.PayDetail = newPayMent.PaymentMethod + "-Deposits: " + newPayMent.Deposits;
            iPaymentService.AddPayment(newPayMent);

            return Ok(orderInfo);
        }

        // DELETE: api/Order/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblOrder(int id)
        //{
        //    var tblOrder = await iOrderService.TblOrders.FindAsync(id);
        //    if (tblOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    iOrderService.TblOrders.Remove(tblOrder);
        //    await iOrderService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblOrderExists(int id)
        //{
        //    return iOrderService.TblOrders.Any(e => e.OrderId == id);
        //}
    }
}
