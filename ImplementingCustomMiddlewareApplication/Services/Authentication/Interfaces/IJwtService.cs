using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementingCustomMiddlewareApplication.Services.Authentication.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(string userName);
    }
}
