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
using Services.DTOs.Response;


namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        
        public AccountsController(IAccountService accountService, ICustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Services.DTOs.Request.LoginRequest request)
        {
            var account = await _accountService.AuthenticateAsync(request.Username, request.Password);
            if (account == null)
            {
                return Unauthorized();
            }
            var loginResponse = new LoginResponse() { Username = account.Username, Role = account.Role };
            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Services.DTOs.Request.RegisterRequest request)
        {
            await _accountService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("CheckUsernameExist")]
        public async Task<IActionResult> CheckUsernameExist(string username)
        {
            var isUsernameExist = _accountService.IsUsernameExisted(username);
            return Ok(isUsernameExist);
        }

        [HttpPost("CheckEmailExist")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var isEmailExist = _customerService.IsEmailExisted(email);
            return Ok(isEmailExist);
        }

        [HttpPost("CheckPhoneExist")]
        public async Task<IActionResult> CheckPhoneExist(string phone)
        {
            var isPhoneExist = _customerService.isPhoneExisted(phone);
            return Ok(isPhoneExist);
        }
    }
}
