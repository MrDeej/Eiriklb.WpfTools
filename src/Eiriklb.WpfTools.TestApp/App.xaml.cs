using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Eiriklb.WpfTools.Logviewer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eiriklb.WpfTools.TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = default!;
        public static IConfigurationRoot Configuration { get; private set; } = default!;

        protected override async void OnStartup(StartupEventArgs e)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");


            Configuration = builder.Build();


            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection, Configuration);

            ServiceProvider = serviceCollection.BuildServiceProvider();


            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

        }

        private void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddWpfTools();
            services.AddScoped<MainWindow>();
        }

    }

}
