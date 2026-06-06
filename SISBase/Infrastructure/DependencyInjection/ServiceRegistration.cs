using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISBase.Domain.Interfaces;
using SISBase.Infrastructure.Persistence.Sqlite;
using SISBase.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SISBase.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("Sqlite"));
            });

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddTransient<RoleViewModel>();

            return services;
        }
    }
}
