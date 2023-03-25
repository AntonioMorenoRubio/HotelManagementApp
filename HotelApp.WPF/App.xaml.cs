using System.IO;
using System.Windows;
using HotelLibrary.Databases;
using HotelLibrary.Interfaces;
using HotelLibrary.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();

            var services = new ServiceCollection();
            services.AddTransient<MainWindow>();
            services.AddTransient<CheckInConfirmationWindow>();

            switch (config.GetValue<string>("DBToUse"))
            {
                case "SQLServer":
                    services.AddTransient<ISqlDataAccess, SqlServerDataAccess>();
                    services.AddTransient<IDatabaseData, SqlData>();
                    break;
                case "SQLite":
                    services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
                    services.AddTransient<IDatabaseData, SqliteData>();
                    break;
                default:
                    break;
            }

            services.AddSingleton(config);

            serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetService<MainWindow>();

            mainWindow.Show();
        }
    }
}
