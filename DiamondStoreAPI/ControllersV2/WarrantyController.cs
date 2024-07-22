using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using BusinessObjects;
using Services;
using GemBox.Document;
using Services.DTOs.Request;
using Services.DTOs.Response;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v2/Warranty/")]
[ApiController]
public class WarrantiesController : ControllerBase
{
    private readonly IWarrantyService _warrantyService;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;

    public WarrantiesController(IWarrantyService warrantyService, IOrderDetailService orderDetailService, IOrderService orderService,IProductService productService, ICustomerService customerService)
    {
        _warrantyService = warrantyService;
        _orderDetailService = orderDetailService;
        _orderService = orderService;
        _productService = productService;
        _customerService = customerService;
    }


    //[Authorize]
    [HttpGet("WarrantyInfo")]
    public async Task<IActionResult> GetWarrantyInfo(int orderDetailID)
    {
        var warrantyInfor = await GetWarrantyInformation(orderDetailID);
        if (warrantyInfor == null) return NotFound();
        return Ok(warrantyInfor);
    }


    [Authorize(Roles = "SaleSatff")]
    [HttpPost("CreateWarranty")]
    public async Task<IActionResult> CreateWarranty([FromBody] WarrantyRequest request)
    {
        var newWarranty = new TblWarranty()
        {
            OrderDetailId= request.OrderDetailID,
            WarrantyStartDate = request.OrderDate?.Date,
            WarrantyEndDate = request.OrderDate?.Date.AddYears(1),
        };
        var createdWarranty = _warrantyService.AddWarranty(newWarranty);
        var warrantyInfo = await GetWarrantyInformation((int)createdWarranty.OrderDetailId);
        return Ok(warrantyInfo);
    }

    private async Task<WarrantyResponse> GetWarrantyInformation(int orderDetailID)
    {
        var orderDetail = _orderDetailService.GetOrderDetailByID(orderDetailID);
        if (orderDetail == null) { return null; }
        var warranty = _warrantyService.GetWarrantyOrderDetailID(orderDetailID);
        var product = await _productService.GetProductAndPriceByIdAsync(orderDetail.ProductId);
        var order = _orderService.getOrderByOrderID((int)orderDetail.OrderId);
        var customer = _customerService.GetCustomerByID((int)order.CustomerId);
        return new WarrantyResponse()
        {
            WarrantyID = warranty.WarrantyId,
            CustomerName = customer.FirstName + " " + customer.LastName,
            CustomerPhone = customer.PhoneNumber,
            ProductID = product.ProductId,
            ProductName = product.ProductName,
            ProductImage = product.Image,
            Category = product.Category,
            Material = product.Material,
            MaterialWeight = (double) product.Weight,
            GemCaratWeight = (double) product.CaratWeight,
            GemOrigin = product.GemOrigin,
            StartDate = (DateTime)warranty.WarrantyStartDate,
            EndDate = (DateTime)warranty.WarrantyEndDate,
        };
    }
}


