using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Request;

namespace Services.DTOs.Response
{
    public class OrderInfo
    {
        public int? OrderID { get; set; }
        public int? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? Address { get; set; }
        public string? Payment {  get; set; }
        public double? Deposits { get; set; } = 0;
        public string? SaleStaff { get; set; } = null;
        public string? Shipper { get; set; } = null;
        public List<ProductBuyingResponse> products { get; set; } = new List<ProductBuyingResponse>();
        public double? TotalPrice { get; set; } = 0;
        public double? DiscountRate { get; set; }
        public double? FinalPrice { get; set; } = 0;
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string? OrderNote { get; set; }
    }
}
