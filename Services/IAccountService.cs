using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IAccountService
    {
        public List<TblAccount> GetAccounts();

        public TblAccount GetAccount(int id);

        public bool AddAccount(TblAccount account);

        public bool UpdateAccount(int id, TblAccount account);
    }
}
