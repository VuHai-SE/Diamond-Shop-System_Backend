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

[ApiController]
[Route("api/[controller]")]
public class WarrantyController : ControllerBase
{
    private readonly IWarrantyService _warrantyService;

    public WarrantyController(IWarrantyService warrantyService)
    {
        _warrantyService = warrantyService;
    }

    [HttpGet("WarrantyInfo")]
    public async Task<IActionResult> GetWarrantyInfo(int orderDetailID)
    {
        var warrantyInfor = await _warrantyService.GetWarrantyInfo(orderDetailID);
        if (warrantyInfor == null) return NotFound();
        return Ok(warrantyInfor);
    }

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
        var warrantyInfo = await _warrantyService.GetWarrantyInfo((int)createdWarranty.OrderDetailId);
        return Ok(warrantyInfo);
    }
}


