using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IWarrantyRepository _warrantyRepository;
        
        public WarrantyService(IWarrantyRepository warrantyRepository)
        {
            _warrantyRepository = warrantyRepository;
        }

        public TblWarranty AddWarranty(TblWarranty warranty)
            => _warrantyRepository.AddWarranty(warranty);

        public List<TblWarranty> GetWarranties()
            => _warrantyRepository.GetWarranties();

        public TblWarranty GetWarrantyByID(int id)
            => _warrantyRepository.GetWarrantyByID(id);

        public TblWarranty GetWarrantyOrderDetailID(int orderDetailID)
            => _warrantyRepository.GetWarrantyOrderDetailID(orderDetailID);

        public void SaveWarrantyImg(int warrantyId, byte[] bytes)
        {
            _warrantyRepository.SaveWarrantyImg(warrantyId, bytes);
        }

        //public async Task<WarrantyResponse> GetWarrantyInfo(int orderDetailID)
        //{
        //    var orderDetail = _orderDetailRepository.GetOrderDetailByID(orderDetailID);
        //    if (orderDetail == null) { return null;}
        //    var warranty = _warrantyRepository.GetWarrantyOrderDetailID(orderDetailID);
        //    var product = await _productRepository.GetProductByIdAsync(orderDetail.ProductId);
            
        //    var order = _orderRepository.getOrderByOrderID((int)orderDetail.OrderId);
        //    var customer = _customerRepository.GetCustomerByID((int)order.CustomerId);
        //    return new WarrantyResponse()
        //    {
        //        WarrantyID = warranty.WarrantyId,
        //        CustomerName = customer.FirstName + " " + customer.LastName,
        //        CustomerPhone = customer.PhoneNumber,
        //        ProductID = product.ProductId,
        //        ProductName = product.ProductName,
        //        ProductImage = product.Image,
        //        StartDate = (DateTime)warranty.WarrantyStartDate,
        //        EndDate = (DateTime)warranty.WarrantyEndDate,
        //    };
        //}
    }
}
