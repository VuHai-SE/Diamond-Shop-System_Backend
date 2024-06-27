using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using BusinessObjects;
using Services;
using GemBox.Document;
using Services.DTOs.Request;

[ApiController]
[Route("api/[controller]")]
public class WarrantyController : ControllerBase
{
    private readonly IWarrantyService _warrantyService;
    private readonly IOrderDetailService _orderDetailService;

    public WarrantyController(IWarrantyService warrantyService, IOrderDetailService orderDetailService)
    {
        _warrantyService = warrantyService;
        _orderDetailService = orderDetailService;
    }

    [HttpGet("WarrantyInfo")]
    public async Task<IActionResult> GetWarrantyInfo(int orderDetailID)
    {
        var warrantyInfor = await _warrantyService.GetWarrantyInfo(orderDetailID);
        if (warrantyInfor == null) return NotFound();
        return Ok(warrantyInfor);
    }

    [HttpPost("SaveWarrantyImg")]
    public async Task<IActionResult> SaveWarrantyImage(int warrantyId, IFormFile imageFile)
    {
        var warranty = _warrantyService.GetWarrantyByID(warrantyId);
        if (warranty == null)
        {
            return NotFound();
        }
        {
            
        }
        if (imageFile == null || imageFile.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        if (Path.GetExtension(imageFile.FileName).ToLower() != ".jpg")
        {
            return BadRequest("Only JPG images are allowed.");
        }

        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            _warrantyService.SaveWarrantyImg(warrantyId, imageBytes);
        }

        return Ok("Image uploaded successfully.");
    }

    //[HttpPost("SaveWarrantyImg")]
    //public IActionResult SaveWarrantyImage([FromBody] SaveWarrantyImageRequest request)
    //{
    //    var warranty = _warrantyService.GetWarrantyByID(request.WarrantyId);
    //    if (warranty == null)
    //    {
    //        return NotFound();
    //    }

    //    if (string.IsNullOrEmpty(request.Base64Image))
    //    {
    //        return BadRequest("No image data provided.");
    //    }

    //    try
    //    {
    //        byte[] imageBytes = Convert.FromBase64String(request.Base64Image);
    //        _warrantyService.SaveWarrantyImg(request.WarrantyId, imageBytes);
    //    }
    //    catch (FormatException)
    //    {
    //        return BadRequest("Invalid Base64 string.");
    //    }

    //    return Ok("Image uploaded successfully.");
    //}
}
