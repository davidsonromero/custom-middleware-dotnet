using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;

namespace ImplementingCustomMiddlewareApplication.Services.Middlewares.Implementations
{
    public class GenerateRequestGuidMiddlewareService : IGenerateRequestGuidMiddlewareService
    {
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
