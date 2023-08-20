using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extenations
{
    public static class ApplicationServiceExtensions
    {  
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => //add DB context to programm class
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); //configration it
            });
            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            return services;

        }
    }
}