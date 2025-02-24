using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces
{
    public interface IApiKeyMiddlewareService
    {
        public bool Verify(string receivedKey);
    }
}
