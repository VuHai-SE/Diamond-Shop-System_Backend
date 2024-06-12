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
    }
}
