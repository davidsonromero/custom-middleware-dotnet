using ImplementingCustomMiddlewareApplication.Services.Middlewares.Implementations;
using ImplementingCustomMiddlewareApplication.Services.Middlewares.Interfaces;

namespace ImplementingCustomMiddlewareApi.Extensions
{
    public static class MiddlewareServicesExtensions
    {
        public static void AddCustomMiddlewareServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IGenerateRequestGuidMiddlewareService, GenerateRequestGuidMiddlewareService>();
            services.AddTransient<IApiKeyMiddlewareService, ApiKeyMiddlewareService>(provider => new ApiKeyMiddlewareService(configuration));
            services.AddTransient<IExceptionHandlingMiddlewareService, ExceptionHandlingMiddlewareService>();
        }
    }
}
