using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Microsoft.AspNetCore.Identity.Data;
using Services.DTOs.Request;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Configuration;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Services.DTOs.Request.LoginRequest request)
        {
            var account = await _accountService.AuthenticateAsync(request.Username, request.Password);
            if (account == null)
            {
                return Unauthorized();
            }

            return Ok(account);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Services.DTOs.Request.RegisterRequest request)
        {
            await _accountService.RegisterAsync(request.Username, request.Password);
            return Ok();
        }

    //{
    //    private readonly DiamondStoreContext _context;

    //    public AccountsController(DiamondStoreContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/Accounts
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<TblAccount>>> GetTblAccounts()
    //    {
    //        return await _context.TblAccounts.ToListAsync();
    //    }

    //    // GET: api/Accounts/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<TblAccount>> GetTblAccount(int id)
    //    {
    //        var tblAccount = await _context.TblAccounts.FindAsync(id);

    //        if (tblAccount == null)
    //        {
    //            return NotFound();
    //        }

    //        return tblAccount;
    //    }

    //    // PUT: api/Accounts/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutTblAccount(int id, TblAccount tblAccount)
    //    {
    //        if (id != tblAccount.AccountId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(tblAccount).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!TblAccountExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/Accounts
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<TblAccount>> PostTblAccount(TblAccount tblAccount)
    //    {
    //        _context.TblAccounts.Add(tblAccount);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetTblAccount", new { id = tblAccount.AccountId }, tblAccount);
    //    }

    //    // DELETE: api/Accounts/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteTblAccount(int id)
    //    {
    //        var tblAccount = await _context.TblAccounts.FindAsync(id);
    //        if (tblAccount == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.TblAccounts.Remove(tblAccount);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool TblAccountExists(int id)
    //    {
    //        return _context.TblAccounts.Any(e => e.AccountId == id);
    //    }
    }
}
