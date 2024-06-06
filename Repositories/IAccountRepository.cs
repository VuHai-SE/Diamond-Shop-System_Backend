﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        Task<TblAccount> GetAccountByUsernameAsync(string username);
        Task AddAccountAsync(TblAccount account);
    }
}
