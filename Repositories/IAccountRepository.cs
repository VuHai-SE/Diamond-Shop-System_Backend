using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IAccountRepository
    {
        public List<TblAccount> GetAccounts();

        public TblAccount GetAccount(int id);

        public bool AddAccount(TblAccount account);

        public bool UpdateAccount(int id, TblAccount account);
    }
}
