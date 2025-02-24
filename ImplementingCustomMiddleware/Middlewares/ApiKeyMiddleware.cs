using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;
using System.Net;

namespace ImplementingCustomMiddlewareApi.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string receivedKey = context.Request?.Query["api_key"];

            IApiKeyMiddlewareService service = context.RequestServices.GetRequiredService<IApiKeyMiddlewareService>();

            if(!service.Verify(receivedKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid API key.");
            }

            await _next(context);
        }
    }

    public static class ApiKeiMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
