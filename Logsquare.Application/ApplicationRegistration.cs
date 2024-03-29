﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logsquare.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IWebApplicationBuilder services, IConfiguration configuration)
        {
            var assemblies = Assembly.Load("Logsquare.Query");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddQuery(configuration);
            return services;
        }
    }


}