namespace ImplementingCustomMiddlewareApi.Middlewares
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class JwtValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && ValidateToken(token, out ClaimsPrincipal principal))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or absent token.");
            }
        }

        private bool ValidateToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JwtConfig:Issuer"],
                    ValidAudience = _configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class JwtValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtValidationMiddleware>();
        }
    }
}
