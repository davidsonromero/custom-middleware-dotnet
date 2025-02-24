using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Implementations
{
    public class ApiKeyMiddlewareService : IApiKeyMiddlewareService
    {
        private readonly IConfiguration _configuration;

        public ApiKeyMiddlewareService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Verify(string receivedKey)
        {
            string expectedKey = _configuration["AppSettings:ApiKey"];

            if (String.IsNullOrEmpty(receivedKey) || !receivedKey.Equals(expectedKey))
            {
                return false;
            }

            return true;
        }
    }
}
