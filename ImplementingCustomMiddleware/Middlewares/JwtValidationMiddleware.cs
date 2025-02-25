namespace ImplementingCustomMiddlewareApi.Middlewares
{
    using ImplementingCustomMiddlewareApplication.Services.Middlewares.Implementations;
    using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;
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

            IJwtMiddlewareService service = context.RequestServices.GetRequiredService<IJwtMiddlewareService>();

            if (token != null && service.ValidateToken(token, out ClaimsPrincipal principal))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or absent token.");
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
