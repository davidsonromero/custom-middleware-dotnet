using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces
{
    public interface IJwtMiddlewareService
    {
        bool ValidateToken(string token, out ClaimsPrincipal principal);
    }
}
