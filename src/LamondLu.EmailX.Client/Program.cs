using LamondLu.EmailX.Client.Extensions;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Infrastructure.DataPersistent;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Client
{
    internal class Program
    {
        private const string _appsettings = "appsettings.json";

        private static async Task Main(string[] args)
        {
            IHost host = new HostBuilder().ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.SetBasePath(Directory.GetCurrentDirectory());
                configApp.AddJsonFile(_appsettings, optional: false, reloadOnChange: true);
                configApp.AddCommandLine(args);
            }).ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
            }).ConfigureEmailX().UseConsoleLifetime().Build();

            await host.RunAsync();
        }
    }
}
