using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<TblAccount> AuthenticateAsync(string username, string password);
        Task RegisterAsync(string username, string password);
        Task<TblAccount> GetAccountByUsernameAsync(string username);
    }
}
