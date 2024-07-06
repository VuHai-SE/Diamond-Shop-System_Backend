using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
        private readonly ISaleStaffRepository _saleStaffRepository;
        private readonly IShipperRepository _shipperRepository;
        private readonly IWarrantyRepository _warrantyRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IProductMaterialRepository productMaterialRepository, IMaterialCategoryRepository materialCategoryRepository, IPaymentRepository paymentRepository, IAccountRepository accountRepository, ISaleStaffRepository saleStaffRepository, IShipperRepository shipperRepository, IWarrantyRepository warrantyRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productMaterialRepository = productMaterialRepository;
            _materialCategoryRepository = materialCategoryRepository;
            _paymentRepository = paymentRepository;
            _accountRepository = accountRepository;
            _saleStaffRepository = saleStaffRepository;
            _shipperRepository = shipperRepository;
            _warrantyRepository = warrantyRepository;
        }

        public async Task<bool> UpdateOrderStatus(OrderStatusRequest request)
        {
            var order = await _orderRepository.GetOrderById((int)request.OrderID);
            if (order == null)
            {
                return false;
            }
            var username = request.Username.Trim();
            var btValue = request.ButtonValue.Trim();
            //neu la sale staff thi add sale staff id vao truong staff id
            //neu la shipper thi add shipper id vao truong shipper id
            //dong thoi add shipping date va received date
            if (request.Role.Equals("SaleStaff"))
            {
                if (string.IsNullOrEmpty(order.StaffId))
                {
                    var SaleStaffID = _saleStaffRepository.GetSaleStaffByUsername(username).StaffId;
                    order.StaffId = SaleStaffID;
                }
                order.OrderStatus = HandleOrderStatus(btValue);
                if (request.ButtonValue.Equals("CANCEL"))
                {
                    //OrderNote
                    order.ShipStatus = request.Username + "-" + "-Cancelled";
                }
            }
            else if (request.Role.Equals("Shipper"))
            {
                if (string.IsNullOrEmpty(order.ShipperId))
                {
                    var ShipperID = _shipperRepository.GetShipperByUsername(username).ShipperId;
                    order.ShipperId = ShipperID;
                }
                order.OrderStatus = HandleOrderStatus(btValue);
                if (btValue.Equals("PICKUP"))
                {
                    order.ShippingDate = request.ShippingDate;
                }
                else if (btValue.Equals("DONE"))
                {
                    order.ReceiveDate = request.ReceivedDate;
                    order.ShipStatus = _accountRepository.GetAccountSaleStaff(order.StaffId).Username
                        + "," + _accountRepository.GetAccountShipper(order.ShipperId).Username + "-Done";
                    var orderInfor = GetOrderInfo(order.OrderId);
                    foreach (var product in orderInfor.products)
                    {
                        var newWarranty = new TblWarranty()
                        {
                            OrderDetailId = product.OrderDetailID,
                            WarrantyStartDate = orderInfor.OrderDate.Date,
                            WarrantyEndDate = orderInfor.OrderDate.Date.AddYears(1),
                        };
                        var createdWarranty = _warrantyRepository.AddWarranty(newWarranty);
                    }
                }
                else if (request.ButtonValue.Equals("CANCEL"))
                {
                    //OrderNote
                    order.ShipStatus = request.Username + "-Cancelled";
                }
            }
            return await _orderRepository.UpdateOrder(order);
        }

        private string HandleOrderStatus(string btValue)
        {
            if (btValue.Equals("CONFIRM"))
            {
                return "Accepted";
            }
            else if (btValue.Equals("READY"))
            {
                return "Pending Delivery";
            }
            else if (btValue.Equals("PICKUP"))
            {
                return "Deliverying";
            }
            else if (btValue.Equals("DONE"))
            {
                return "Deliveried";
            }
            else
            {
                return "Cancelled";
            }
        }

        public async Task<TblProduct> CreateProductAsync(TblProduct product)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return product;
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
                orderInfo.Deposits = _paymentRepository.GetPaymentByCustomerAndOrder(orderID, customer.CustomerId).Deposits;
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
                orderInfo.DiscountRate = customer.DiscountRate;
                orderInfo.OrderDate = (DateTime)order.OrderDate;
                orderInfo.OrderStatus = order.OrderStatus;
                orderInfo.ShippingDate = order.ShippingDate;
                orderInfo.ReceiveDate = order.ReceiveDate;
                orderInfo.OrderNote = order.ShipStatus;
                var OrderDetail = _orderDetailRepository.GetOrderDetailsByOrderID(order.OrderId);
                foreach (var orderDetail in OrderDetail)
                {
                    orderInfo.TotalPrice += (double)orderDetail.TotalPrice;
                    orderInfo.FinalPrice += (double)orderDetail.FinalPrice;
                    var product = _productRepository.GetProduct(orderDetail.ProductId);
                    var productMaterial = _productMaterialRepository.GetProductMaterialProductID(product.ProductId);
                    orderInfo.products.Add(new ProductBuyingResponse()
                    {
                        OrderDetailID = orderDetail.OrderDetailId,
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

        public List<OrderInfo> GetOrderInfoListForSaleStaff()
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

        public List<OrderInfo> GetOrderInforListForShipper()
        {
            var orderInforList = new List<OrderInfo>();
            var orders = _orderRepository.GetOrders();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    if (order.OrderStatus.Equals("Pending Delivery") ||
                        order.OrderStatus.Equals("Deliverying") ||
                        order.OrderStatus.Equals("Deliveried") ||
                        (order.OrderStatus.Equals("Cancelled") && !order.ShipperId.IsNullOrEmpty()))
                    {
                        var orderInfo = GetOrderInfo(order.OrderId);
                        orderInforList.Add(orderInfo);
                    }
                }
            }
            return orderInforList;
        }
        public Task<bool> UpdateOrder(TblOrder order)
           => _orderRepository.UpdateOrder(order);
    
public int GetSumOrderByMonth(int month, int year)
        {
            var orders = _orderRepository.GetOrders();

            // Filter orders by the specified month and year
            var filteredOrders = orders.Where(o => o.OrderDate.HasValue
                                                    && o.OrderDate.Value.Month == month
                                                    && o.OrderDate.Value.Year == year && o.ShipStatus == "delivered");

            // Return the count of filtered orders
            return filteredOrders.Count();
        }

        public int GetStaffs()
        {
            var staffMembers = _accountRepository.GetAllStaff();
            return staffMembers.Count;
        }

        public async Task<decimal> GetSumRevenue(int month, int year)
        {
            var deliveredOrders = await _orderRepository.GetDeliveredOrdersByMonthAndYearAsync(month, year);
            decimal totalRevenue = 0;

            foreach (var order in deliveredOrders)
            {
                var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderID(order.OrderId);
                totalRevenue += orderDetails.Sum(od => (decimal)(od.FinalPrice ?? 0));
            }

            return totalRevenue;
        }
    }
}
