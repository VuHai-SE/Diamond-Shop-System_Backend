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
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public WarrantyService(IWarrantyRepository warrantyRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _warrantyRepository = warrantyRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
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

        public async Task<WarrantyResponse> GetWarrantyInfo(int orderDetailID)
        {
            var orderDetail = _orderDetailRepository.GetOrderDetailByID(orderDetailID);
            if (orderDetail == null) { return null;}
            var warranty = _warrantyRepository.GetWarrantyOrderDetailID(orderDetailID);
            var product = await _productRepository.GetProductByIdAsync(orderDetail.ProductId);
            var order = _orderRepository.getOrderByOrderID((int)orderDetail.OrderId);
            var customer = _customerRepository.GetCustomerByID((int)order.CustomerId);
            return new WarrantyResponse()
            {
                WarrantyID = warranty.WarrantyId,
                CustomerName = customer.FirstName + " " + customer.LastName,
                ProductID = product.ProductId,
                ProductName = product.ProductName,
                StartDate = (DateTime)warranty.WarrantyStartDate,
                EndDate = (DateTime)warranty.WarrantyEndDate,
            };
        }
    }
}
