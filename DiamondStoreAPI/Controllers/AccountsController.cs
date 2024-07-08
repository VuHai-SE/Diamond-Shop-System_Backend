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


namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly string _jwtSecret;
        private readonly IConfiguration _configuration;
        private readonly ISaleStaffService _saleStaffService;
        private readonly IShipperService _shipperService;


        public AccountsController(IAccountService accountService, ICustomerService customerService, IConfiguration configuration)
        {
            _accountService = accountService;
            _customerService = customerService;
            _configuration = configuration;
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


        [HttpPost("CheckUsernameExist")]
        public async Task<IActionResult> CheckUsernameExist(string username)
        {
            return Ok(_accountService.IsUsernameExisted(username));
        }

        [HttpPost("CheckEmailExist")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            return Ok(_customerService.IsEmailExisted(email));
        }

        [HttpPost("CheckPhoneExist")]
        public async Task<IActionResult> CheckPhoneExist(string phone)
        {
            return Ok(_customerService.isPhoneExisted(phone));
        }

        [HttpGet("GetAccountList")]
        public async Task<IActionResult> GetAccountList()
        {
            var accountList = await _accountService.GetAccountInfoList();
            if (accountList.IsNullOrEmpty()) return NotFound();
            return Ok(accountList);
        }

        [HttpGet("GetCustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {
            var customerList = await _accountService.GetCustomerInfoList();
            if (customerList.IsNullOrEmpty()) return NotFound();
            return Ok(customerList);
        }

        [HttpGet("GetSaleStaffList")]
        public async Task<IActionResult> GetSaleStaffList()
        {
            var saleStaffList = await _accountService.GetSaleInfoList();
            if (saleStaffList.IsNullOrEmpty()) return NotFound();
            return Ok(saleStaffList);
        }

        [HttpGet("GetShipperList")]
        public async Task<IActionResult> GetShipperList()
        {
            var shipperList = await _accountService.GetShipperInfoList();
            if (shipperList.IsNullOrEmpty()) return NotFound();
            return Ok(shipperList);
        }

        [HttpPut("ChangeRole")]
        public async Task<IActionResult> ChangeRole([FromBody] UpdateRoleRequest request)
        {
            var isSuccess = await _accountService.ChangeAccountRole(request);
            if (!isSuccess) return BadRequest("Update fail");
            return Ok(request.UsertName + "'s role has change into " + request.Role);
        }

        [HttpPut("UpdateAccountStatus")]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] UpdateAccountStatusRequest request)
        {
            var isSuccess = await _accountService.UpdateAccountStatus(request.Username, request.Status);
            if (!isSuccess) return BadRequest("Update fail");
            return Ok(request.Username + "-" + request.Status);
        }

        [HttpPost("AddStaffId")]
        public async Task<IActionResult> AddToStaffTables([FromBody] AddStaffTables request)
        {
            var accountInfo = await _accountService.GetAccountInfo(request.Username);
            if (accountInfo == null) return NotFound();
            _accountService.AddToStaffTables(request.StaffId, accountInfo);
            return Ok(request.StaffId + " has been added");
        }

        [HttpPost("CreateStaffAccount")]
        public async Task<IActionResult> CreateStaffAccount([FromBody] CreateStaffAccountRequest request)
        {
            var registerRequest = new Services.DTOs.Request.RegisterRequest()
            {
                Username = request.Username,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Birthday = request.Birthday,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };
            _accountService.RegisterAsync(registerRequest);
            var updateRoleRequest = new UpdateRoleRequest() { Role = request.Role, UsertName = request.Username };
            var isChanageRole = _accountService.ChangeAccountRole(updateRoleRequest);
            var accountInfo = await _accountService.GetStaffInfo(request.Username);
            _accountService.AddToStaffTables(request.StaffId, accountInfo);
            var staffInfo = _accountService.GetStaffInfo(request.Username);
            return Ok(staffInfo);
        }

        [HttpGet("CheckSaleStaffIdExist")]
        public async Task<IActionResult> CheckSaleStaffIdExist(string saleStaffId)
        {
            bool isExist = _saleStaffService.isSaleStaffIdExist(saleStaffId);
            return Ok(isExist);
        }

        [HttpGet("CheckShipperIdExist")]
        public async Task<IActionResult> CheckShipperIdExist(string shipperId)
        {
            bool isExist = _shipperService.IsShipperIdExist(shipperId);
            return Ok(isExist);
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
