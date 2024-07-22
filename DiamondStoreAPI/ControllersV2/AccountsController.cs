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
using BusinessObjects.ResponseModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Serilog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authorization;


namespace DiamondStoreAPI.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/Accounts/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly string _jwtSecret;
        private readonly IConfiguration _configuration;
        private readonly ISaleStaffService _saleStaffService;
        private readonly IShipperService _shipperService;


        public AccountController(IAccountService accountService, ICustomerService customerService, IConfiguration configuration, ISaleStaffService saleStaffService, IShipperService shipperService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _configuration = configuration;
            _saleStaffService = saleStaffService;
            _shipperService = shipperService;
            _jwtSecret = _configuration.GetValue<string>("Jwt:Day_la_key_JWT");
        }


        [HttpPost("LoginGoogle")]
        public async Task<IActionResult> LoginByGoogle(string email)
        {
           
            var account = _accountService.GetAccountByEmail(email);
            if (account == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(account);
            BusinessObjects.ResponseModels.LoginResponse customerInfo = _customerService.GetCustomerByAccountForLogin(account.Username);

            return Ok(new
            {
                Token = token,
                CustomerInfo = customerInfo
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Services.DTOs.Request.LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client request");
            }

            var account = await _accountService.AuthenticateAsync(request.Username, request.Password);
            if (account == null || _customerService.GetCustomerByAccountForLogin(request.Username).Status != true)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(account);
            BusinessObjects.ResponseModels.LoginResponse customerInfo = _customerService.GetCustomerByAccountForLogin(request.Username);

            return Ok(new
            {
                Token = token,
                CustomerInfo = customerInfo
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Services.DTOs.Request.RegisterRequest request)
        {
            await _accountService.RegisterAsync(request);
            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return BadRequest("Token is missing");
            }

            TokenBlacklist.Add(token);
            return Ok("Logged out successfully");
        }


        //[Authorize(Roles = "Customer")]
        [Authorize]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] Services.DTOs.Request.ForgotPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Username, old password, and new password must be provided.");
            }

            var result = await _accountService.ForgotPasswordAsync(request);

            if (result == "Account not found.")
            {
                return NotFound(result);
            }
            else if (result == "Old password does not match." || result == "New password cannot be the same as the old password.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("CheckUsernameExist")]
        public async Task<IActionResult> CheckUsernameExist(string username)
        {
            return Ok(_accountService.IsUsernameExisted(username));
        }

        [Authorize]
        [HttpPost("CheckEmailExist")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            return Ok(_customerService.IsEmailExisted(email));
        }

        [Authorize]
        [HttpPost("CheckPhoneExist")]
        public async Task<IActionResult> CheckPhoneExist(string phone)
        {
            return Ok(_customerService.isPhoneExisted(phone));
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetAccountList")]
        public async Task<IActionResult> GetAccountList()
        {
            var accountList = await _accountService.GetAccountInfoList();
            if (accountList.IsNullOrEmpty()) return NotFound();
            return Ok(accountList);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetCustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {
            var customerList = await _accountService.GetCustomerInfoList();
            if (customerList.IsNullOrEmpty()) return NotFound();
            return Ok(customerList);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetSaleStaffList")]
        public async Task<IActionResult> GetSaleStaffList()
        {
            var saleStaffList = await _accountService.GetSaleInfoList();
            if (saleStaffList.IsNullOrEmpty()) return NotFound();
            return Ok(saleStaffList);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetShipperList")]
        public async Task<IActionResult> GetShipperList()
        {
            var shipperList = await _accountService.GetShipperInfoList();
            if (shipperList.IsNullOrEmpty()) return NotFound();
            return Ok(shipperList);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("ChangeRole")]
        public async Task<IActionResult> ChangeRole([FromBody] UpdateRoleRequest request)
        {
            var isSuccess = await _accountService.ChangeAccountRole(request);
            if (!isSuccess) return BadRequest("Update fail");
            return Ok(request.UsertName + "'s role has change into " + request.Role);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("UpdateAccountStatus")]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] UpdateAccountStatusRequest request)
        {
            var isSuccess = await _accountService.UpdateAccountStatus(request.Username, request.Status);
            if (!isSuccess) return BadRequest("Update fail");
            return Ok(request.Username + "-" + request.Status);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("RegisterStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaff request)
        {
            await _accountService.RegisterStaffAsync(request);
            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("AccountCount")]
        public async Task<IActionResult> GetAccountCount()
        {
            var result = await _accountService.GetAccountCount();
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("CustomerRankingCount")]
        public async Task<IActionResult> GetCustomerRankingCount()
        {
            var result = await _accountService.GetCustomerRankingCount();
            return Ok(result);
        }

        private string GenerateJwtToken(TblAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, account.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
