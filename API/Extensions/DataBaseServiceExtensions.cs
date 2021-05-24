using DataService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class DataBaseServiceExtensions
    {
        public static IServiceCollection AddDataBaseServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(data =>
            {
                data.UseSqlServer(config.GetConnectionString("Default"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });   

            return services;
        }
    }
}