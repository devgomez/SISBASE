using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SISBase.Infrastructure.DependencyInjection;
using SISBase.Infrastructure.Persistence.Sqlite;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SISBase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(    StartupEventArgs e)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddInfrastructure(configuration);

            Services = services.BuildServiceProvider();

            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var effectiveConnection = dbContext.Database.GetConnectionString();
                var resolvedDb = DbDiagnostics.ResolveDbPath(effectiveConnection);

                DbDiagnostics.Log($"EF EffectiveConnectionString: {effectiveConnection}");
                DbDiagnostics.Log($"EF ResolvedDatabasePath: {resolvedDb ?? "<null>"}");

                var created = dbContext.Database.EnsureCreated();
                DbDiagnostics.Log($"EnsureCreatedResult: {created}");
            }

            base.OnStartup(e);
        }
    }

}
