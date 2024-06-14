using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Identity.Client;
using PayPal.Api;
using Repositories;
using Repositories.Implement;
using Services.DTOs.Request;
using Services.DTOs.Response;

namespace Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductMaterialRepository _productMaterialRepository;
        private readonly IMaterialCategoryRepository _materialCategoryRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAccountRepository _accountRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IProductMaterialRepository productMaterialRepository, IMaterialCategoryRepository materialCategoryRepository, IPaymentRepository paymentRepository, IAccountRepository accountRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productMaterialRepository = productMaterialRepository;
            _materialCategoryRepository = materialCategoryRepository;
            _paymentRepository = paymentRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return false;
            }
            order.OrderStatus = status;
            return await _orderRepository.UpdateOrder(order);
        }

        public TblOrder AddOrder(TblOrder order)
            => _orderRepository.AddOrder(order);

        public void CancelOrder(int orderID)
            => _orderRepository.CancelOrder(orderID);

        public List<TblOrder> GetOrdersByCustomerID(int customerID)
            => _orderRepository.getOrderByCustomerID(customerID);

        public TblOrder getOrderByOrderID(int orderID)
            => _orderRepository.getOrderByOrderID(orderID);

        public List<OrderInfo> GetOrderHistory(string username)
        {
            var orderHistory = new List<OrderInfo>();
            var customer = _customerRepository.GetCustomerByAccount(username);
            int customerID = customer.CustomerId;
            var orders = GetOrdersByCustomerID(customerID);
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderInfo = GetOrderInfo(order.OrderId);
                    orderHistory.Add(orderInfo);
                }
            }
            return orderHistory;
        }

        public List<TblOrder> GetOrders()
            => _orderRepository.GetOrders();
        public OrderInfo GetOrderInfo(int orderID)
        {
            var orderInfo = new OrderInfo();
            var order = _orderRepository.getOrderByOrderID(orderID);
            if (order != null)
            {
                var customer = _customerRepository.GetCustomerByID((int)order.CustomerId);
                
                orderInfo.OrderID = order.OrderId;
                orderInfo.CustomerID = customer.CustomerId;
                orderInfo.CustomerName = customer.FirstName + " " + customer.LastName;
                orderInfo.CustomerPhone = customer.PhoneNumber;
                orderInfo.Address = customer.Address;
                orderInfo.Payment = order.PaymentMethod;
                orderInfo.Deposits = (double)_paymentRepository.GetPaymentByCustomerAndOrder(orderID, customer.CustomerId).Deposits;
                if (order.StaffId != null)
                {
                    var accSaleStaff = _accountRepository.GetAccountSaleStaff(order.StaffId);
                    orderInfo.SaleStaff = accSaleStaff.Username;
                }
                if (order.ShipperId != null)
                {
                    var accShipper = _accountRepository.GetAccountShipper(order.ShipperId);
                    orderInfo.Shipper = accShipper.Username;
                }
                orderInfo.DiscountRate = (double)customer.DiscountRate;
                orderInfo.OrderDate = (DateTime)order.OrderDate;
                orderInfo.OrderStatus = (string)order.OrderStatus;
                var OrderDetail = _orderDetailRepository.GetOrderDetailsByOrderID(order.OrderId);
                foreach (var orderDetail in OrderDetail)
                {
                    orderInfo.TotalPrice += (double)orderDetail.TotalPrice;
                    orderInfo.FinalPrice += (double)orderDetail.FinalPrice;
                    var product = _productRepository.GetProduct(orderDetail.ProductId);
                    var productMaterial = _productMaterialRepository.GetProductMaterialProductID(product.ProductId);
                    orderInfo.products.Add(new ProductBuyingResponse()
                    {
                        ProductID = product.ProductId,
                        ProductName = product.ProductName,
                        Material = _materialCategoryRepository.GetMaterialCategory(productMaterial.MaterialId).MaterialName,
                        Image = product.Image,
                        CustomizedSize = (int)orderDetail.CustomizedSize,
                        Quantity = (int)orderDetail.Quantity,
                        Price = (double)orderDetail.TotalPrice
                    }
                        );
                }
            }
            return orderInfo;
        }

        public List<OrderInfo> GetOrderInfoList()
        {
            var orderInforList = new List<OrderInfo>();
            var orders = _orderRepository.GetOrders();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderInfo = GetOrderInfo(order.OrderId);
                    orderInforList.Add(orderInfo);
                }
            }
            return orderInforList;
        }

        public List<OrderInfo> GetAcceptedOrderInforList()
        {
            var orderInforList = new List<OrderInfo>();
            var orders = _orderRepository.GetOrders();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    if (order.OrderStatus.Equals("Accepted"))
                    {
                        var orderInfo = GetOrderInfo(order.OrderId);
                        orderInforList.Add(orderInfo);
                    }
                }
            }
            return orderInforList;
        }
    }
}
