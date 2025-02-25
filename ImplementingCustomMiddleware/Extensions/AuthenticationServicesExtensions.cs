using ImplementingCustomMiddlewareApplication.Services.Authentication.Implementations;
using ImplementingCustomMiddlewareApplication.Services.Authentication.Interfaces;

namespace ImplementingCustomMiddlewareApi.Extensions
{
    public static class AuthenticationServicesExtensions
    {
        public static void AddCustomAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJwtService, JwtService>(provider => new JwtService(configuration));
        }
    }
}
