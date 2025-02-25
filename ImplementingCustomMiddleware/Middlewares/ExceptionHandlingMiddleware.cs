using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;
using Microsoft.Data.SqlClient;

namespace ImplementingCustomMiddlewareApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IExceptionHandlingMiddlewareService service = context.RequestServices.GetService<IExceptionHandlingMiddlewareService>();
            try
            {
                await _next(context);
            }
            catch (ArgumentNullException ex)
            {
                await service.HandleExceptionAsync(context, ex, "An invalid argument has been given. Please, review your entry data.");
            }
            catch (ArgumentException ex)
            {
                await service.HandleExceptionAsync(context, ex, "A required argument is missing. Please, verify if every required field is filled.");
            }
            catch (InvalidOperationException ex)
            {
                await service.HandleExceptionAsync(context, ex, "The requested operation is not valid at the current application state. Please, try again later.");
            }
            catch (NotImplementedException ex)
            {
                await service.HandleExceptionAsync(context, ex, "This feature has not been implemented yet. Please, contact support for more information.");
            }
            catch (UnauthorizedAccessException ex)
            {
                await service.HandleExceptionAsync(context, ex, "Unauthorized access. Please, review your credentials and try again later.");
            }
            catch (HttpRequestException ex)
            {
                await service.HandleExceptionAsync(context, ex, "There was a problem while processing an HTTP request. Please, try again later.");
            }
            catch (TimeoutException ex)
            {
                await service.HandleExceptionAsync(context, ex, "The operation has exceeded the time limit. Please, try again later.");
            }
            catch (SqlException ex)
            {
                await service.HandleExceptionAsync(context, ex, "There was a problem while accessing the database. Please, try again later.");
            }
            catch (Exception ex)
            {
                await service.HandleExceptionAsync(context, ex, "There was an unexpected problem while processing the request. Please, contact support.");
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
