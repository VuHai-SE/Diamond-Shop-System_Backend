using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")] // Protect this endpoint for Admin and Manager roles
    public class SomeProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is a protected endpoint for Admin and Manager roles");
        }
    }
}
