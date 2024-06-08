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

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService, ICustomerService customerService, IPaymentService paymentService)
        {
            iOrderService = orderService;
            iOrderDetailService = orderDetailService;
            iProductService = productService;
            iCustomerService = customerService;
            iPaymentService = paymentService;
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
            TblOrder newOrder = new TblOrder()
            {
                CustomerId = newOrderRequest.CustomerId,
                PaymentMethod = newOrderRequest.PaymentMethod,
                OrderDate = newOrderRequest.OrderDate,
                OrderStatus = newOrderRequest.PaymentMethod.Equals("Cash on Delivery") ? "Processing" : "Accepted",
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
                ProductWithPriceResponse productWithPrice = await iProductService.GetProductAndPriceByIdAsync(productID);
                TblProduct product = productWithPrice.product;
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
                newOrderDetail.TotalPrice = productWithPrice.price * p.Quantity + newOrderDetail.CustomizedAmount;

                newOrderDetail.FinalPrice = (1 - customer.DiscountRate) * newOrderDetail.TotalPrice;
                var orderDetail = iOrderDetailService.AddOrderDetail(newOrderDetail);
                orderInfo.products.Add(p);
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
