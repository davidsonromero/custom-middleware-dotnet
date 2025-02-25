using ImplementingCustomMiddlewareApplication.DTOs.ExceptionHandling;
using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Implementations
{
    public class ExceptionHandlingMiddlewareService : IExceptionHandlingMiddlewareService
    {
        public Task HandleExceptionAsync(HttpContext context, Exception exception, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message + "\n" + exception.Message
            }.ToString());
        }
    }
}
