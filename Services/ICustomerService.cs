﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.ResponseModels;

namespace Services
{
    public interface ICustomerService
    {
        public List<TblCustomer> GetCustomers();
        public TblCustomer GetCustomerByID(int customerID);
        public TblCustomer GetCustomerByAccount(string username);
        public LoginResponse GetCustomerByAccountForLogin(string username);
        public bool IsEmailExisted(string email);
        public bool isPhoneExisted(string phone);
    }
}
