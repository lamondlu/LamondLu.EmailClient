using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    class Program
    {
        private const string _appsettings = "appsettings.json";

        private static async Task Main(string[] args)
        {
            IHost host = new HostBuilder().ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.SetBasePath(Directory.GetCurrentDirectory());
                configApp.AddJsonFile(_appsettings, optional: true, reloadOnChange: true);
                configApp.AddJsonFile(
                    $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                    optional: true);
                configApp.AddCommandLine(args);
            }).ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>();
                services.AddHostedService<EmailConnectorHostService>();

            }).UseConsoleLifetime().Build();

            await host.RunAsync();
        }
    }
}
