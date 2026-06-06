using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var sqliteConnection = configuration.GetConnectionString("Sqlite");
            DbDiagnostics.LogStartupInfo(sqliteConnection);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(sqliteConnection);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(DbDiagnostics.LogEf, LogLevel.Information);
            });

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();

            services.AddTransient<RoleViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<BrandViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<UnitViewModel>();
            services.AddTransient<OptionViewModel>();

            return services;
        }
    }
}
