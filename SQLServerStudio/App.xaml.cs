using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SQLServerStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Encapsulates resources and functionality (such as dependency injection)
        /// </summary>
        private IHost _host;

        public App()
        {
            //Initailze Host and services
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context,services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }


        /// <summary>
        /// Configures application services
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
        private void ConfigureServices (IServiceCollection services)
        {
            services.AddSingleton<IDialogService,DialogService>();
            services.AddSingleton<IMessageService,MessageService>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        }

        /// <summary>
        /// Raises startup application event
        /// </summary>
        /// <param name="e">Instance of <see cref="StartupEventArgs"/></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            //Set and open main application window
            var window = _host.Services.GetRequiredService<MainWindow>();
            window.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
            window.Show();

            base.OnStartup(e);
  
        }

        /// <summary>
        /// Raises exit application event
        /// </summary>
        /// <param name="e">Instance of <see cref="ExitEventArgs"/></param>
        protected override async void OnExit(ExitEventArgs e)
        {
            //Stop and dispose host
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
