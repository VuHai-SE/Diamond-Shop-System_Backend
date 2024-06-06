using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Services.DTOs;
using DAOs;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService iAccountService;

        public AccountsController(DiamondStoreContext context)
        {
            iAccountService = new AccountService();
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAccount>>> GetTblAccounts()
        {
            var accounts = iAccountService.GetAccounts();
            if(accounts == null)
            {
                return NotFound();
            }
            
            return accounts;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetTblAccount(int id)
        {
            var accounts = iAccountService.GetAccounts();
            if (accounts == null)
            {
                return NotFound();
            }

            var account = iAccountService.GetAccount(id);

            if (account == null)
            {
                return NotFound();
            }

            // Check the role of the account and return the appropriate DTO
            switch (account.Role)
            {
                case "Customer":
                    var customerDto = new CustomerDTO
                    {
                        AccountId = account.AccountId,
                        Username = account.Username,
                        Role = account.Role,
                        CustomerId = account.TblCustomer?.CustomerId,
                        FirstName = account.TblCustomer?.FirstName,
                        LastName = account.TblCustomer?.LastName,
                        Gender = account.TblCustomer?.Gender,
                        Birthday = account.TblCustomer?.Birthday ?? default,
                        Email = account.TblCustomer?.Email,
                        PhoneNumber = account.TblCustomer?.PhoneNumber,
                        Address = account.TblCustomer?.Address,
                        Ranking = account.TblCustomer?.Ranking,
                        DiscountRate = account.TblCustomer?.DiscountRate ?? 0,
                        Status = account.TblCustomer?.Status ?? false
                    };
                    return Ok(customerDto);

                case "Shipper":
                    var shipperDto = new ShipperDTO
                    {
                        AccountId = account.AccountId,
                        Username = account.Username,
                        Role = account.Role,
                        ShipperId = account.TblShipper?.ShipperId,
                        FirstName = account.TblShipper?.FirstName,
                        LastName = account.TblShipper?.LastName
                    };
                    return Ok(shipperDto);

                case "Manager":
                case "SaleStaff":
                case "Admin":
                    var staffDto = new StaffDTO
                    {
                        AccountId = account.AccountId,
                        Username = account.Username,
                        Role = account.Role,
                        StaffId = account.TblSaleStaff?.StaffId,
                        FirstName = account.TblSaleStaff?.FirstName,
                        LastName = account.TblSaleStaff?.LastName
                    };
                    return Ok(staffDto);

                default:
                    return BadRequest("Invalid role");
            }
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblAccount(int id, TblAccount tblAccount)
        //{
        //    if (id != tblAccount.AccountId)
        //    {
        //        return BadRequest();
        //    }

        //    iAccountService.Entry(tblAccount).State = EntityState.Modified;

        //    try
        //    {
        //        await iAccountService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblAccountExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblAccount>> PostTblAccount(TblAccount tblAccount)
        {
            var isAdd = iAccountService.AddAccount(tblAccount);
            return CreatedAtAction("GetTblAccount", new { id = tblAccount.AccountId }, tblAccount);
        }

        // DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblAccount(int id)
        //{
        //    var tblAccount = await iAccountService.TblAccounts.FindAsync(id);
        //    if (tblAccount == null)
        //    {
        //        return NotFound();
        //    }

        //    iAccountService.TblAccounts.Remove(tblAccount);
        //    await iAccountService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblAccountExists(int id)
        //{
        //    return iAccountService.TblAccounts.Any(e => e.AccountId == id);
        //}
    }
}
