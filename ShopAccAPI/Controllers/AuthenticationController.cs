using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ShopAccAPI.Models.Authentication;
using ShopAccAPI.Services;

namespace ShopAccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtService _authService;
        public AuthenticationController(JwtService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginDto)
        {
            var result = await _authService.Authenticate(loginDto);
            if (result == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(result);
        }
    }
}
