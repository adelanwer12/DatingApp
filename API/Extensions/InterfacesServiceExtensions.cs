using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Service;

namespace API.Extensions
{
    public static class InterfacesServiceExtensions
    {
        public static IServiceCollection AddInterfacesServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}