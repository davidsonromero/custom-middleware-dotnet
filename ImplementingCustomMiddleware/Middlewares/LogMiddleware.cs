using System.Diagnostics;

namespace ImplementingCustomMiddlewareApi.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation($"Receiving request: {request.Method} {request.Path} | ID: {request.Headers["X-Transaction-Id"]} | IP Address: {context.Connection.RemoteIpAddress}");

            try
            {
                _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error while executing a request: {request.Method} {request.Path} | ID: {request.Headers["X-Transaction-Id"]} | IP Address: {context.Connection.RemoteIpAddress} | Message: {ex.Message}");
            }

            stopwatch.Stop();
            var response = context.Response;

            _logger.LogInformation($"Sending response: {response.StatusCode} | Time: {stopwatch.ElapsedMilliseconds}ms | ID: {request.Headers["X-Transaction-Id"]} | IP Address: {context.Connection.RemoteIpAddress}");
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogMiddleware>();
        }
    }
}
