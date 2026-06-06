using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISBase.Infrastructure.DependencyInjection;
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

            base.OnStartup(e);
        }
    }

}
