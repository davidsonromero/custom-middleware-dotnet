using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces
{
    public interface IExceptionHandlingMiddlewareService
    {
        Task HandleExceptionAsync(HttpContext context, Exception exception, string message);
    }
}
