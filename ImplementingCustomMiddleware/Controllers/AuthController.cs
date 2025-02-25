using ImplementingCustomMiddlewareApplication.DTOs.Login;
using ImplementingCustomMiddlewareApplication.Services.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingCustomMiddlewareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthController(IConfiguration configuration, IJwtService jwtService)
        {
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            //Simulated username and password validation
            if (loginRequest.UserName == "testuser" && loginRequest.Password == "password")
            {
                var token = _jwtService.GenerateJwtToken(loginRequest.UserName);
                return Ok(new LoginResponseDto
                {
                    UserName = loginRequest.UserName,
                    AccessToken = token,
                    MinutesToExpire = int.Parse(_configuration["JwtConfig:TokenValidityMins"])
                });
            }
            return Unauthorized("Invalid user name or password.");
        }
    }
}
