using Logsquare.Application.Common.Interfaces;
using Logsquare.Infrastructure.Helper;
using Logsquare.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logsquare.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LogsqureDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
                options.LogTo(Console.WriteLine);
            });

            services.AddScoped<LogsqureDbContext, LogsqureDbContext>();
            services.AddScoped<IHashAlgorithm, HashAlgorithm>();
            return services;
        }
    }
   
}
