using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;

namespace ImplementingCustomMiddlewareApi.Middlewares
{
    public class GenerateRequestGuidMiddleware
    {
        private readonly RequestDelegate _next;

        public GenerateRequestGuidMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IGenerateRequestGuidMiddlewareService service = context.RequestServices.GetService<IGenerateRequestGuidMiddlewareService>();

            context.Request.Headers.Append("X-Transaction-Id", service.GenerateGuid());

            _next(context);
        }
    }

    public static class GenerateRequestGuidMiddlewareExtensions
    {
        public static IApplicationBuilder UseGenerateRequestGuidMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GenerateRequestGuidMiddleware>();
        }
    }
}
