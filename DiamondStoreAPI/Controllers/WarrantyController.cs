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

    [HttpPost("create")]
    public async Task<IActionResult> CreateWarranty([FromBody] WarrantyRequest request)
    {
        var orderDetail = _orderDetailService.GetOrderDetailByID(request.OrderDetailID);
        if (orderDetail == null) { return NotFound(); }

        // Kiểm tra xem Warranty đã tồn tại cho OrderDetail này chưa
        var existingWarranty = _warrantyService.GetWarrantyOrderDetailID(request.OrderDetailID);
        if (existingWarranty != null)
        {
            return Conflict("A warranty already exists for this OrderDetailID.");
        }

        var newWarranty = new TblWarranty()
        {
            OrderDetailId = request.OrderDetailID,
            WarrantyStartDate = request.orderDate.Date,
            WarrantyEndDate = request.orderDate.Date.AddYears(1),
        };
        var createdWarranty = _warrantyService.AddWarranty(newWarranty);

        // Tạo PDF từ thông tin bảo hành
        var pdfStream = GenerateWarrantyPdf(createdWarranty);

        // Lưu PDF vào cơ sở dữ liệu
        _warrantyService.SaveWarrantyPdf(createdWarranty.WarrantyId, pdfStream.ToArray());

        return Ok(createdWarranty.WarrantyPdf);
    }

    private MemoryStream GenerateWarrantyPdf(TblWarranty warranty)
    {
        // Đặt giấy phép cho GemBox.Document
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        // Đường dẫn tới mẫu PDF
        string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "warranty.pdf");
        string templatePath = Path.GetFullPath(relativePath); // Chuyển đổi đường dẫn tương đối thành tuyệt đối

        // Tải mẫu PDF
        var document = DocumentModel.Load(templatePath);

        // Điền dữ liệu vào mẫu PDF
        var warrantyIdField = document.Content.Find("ID").FirstOrDefault();
        var productIdField = document.Content.Find("Product id:").FirstOrDefault();
        var productNameField = document.Content.Find("Product name:").FirstOrDefault();
        var warrantyPeriodField = document.Content.Find("Warranty period from:").FirstOrDefault();

        if (warrantyIdField != null)
            warrantyIdField.LoadText($"ID: {warranty.WarrantyId}");
        if (productIdField != null)
            productIdField.LoadText($"Product id: {warranty.OrderDetailId}"); // Cập nhật đúng trường thông tin
        if (productNameField != null)
            productNameField.LoadText("Product name: Example Product"); // Cập nhật đúng trường thông tin
        if (warrantyPeriodField != null)
            warrantyPeriodField.LoadText($"Warranty period from: {warranty.WarrantyStartDate?.ToShortDateString()} To: {warranty.WarrantyEndDate?.ToShortDateString()}");

        // Lưu tài liệu sang PDF
        MemoryStream stream = new MemoryStream();
        document.Save(stream, SaveOptions.PdfDefault);
        stream.Position = 0;

        return stream;
    }
}
