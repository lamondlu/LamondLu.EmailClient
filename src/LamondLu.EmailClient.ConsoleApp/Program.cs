using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.DataPersistent;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    internal class Program
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
                services.AddOptions();
                services.Configure<DbSetting>(hostContext.Configuration.GetSection("Db"));

                services.AddSingleton<ILogger, ConsoleLogger>();
                services.AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>();
                services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
                services.AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>();
                services.AddHostedService<EmailConnectorHostService>();

                EnvironmentConst.Services = services.BuildServiceProvider();

            }).UseConsoleLifetime().Build();

            await host.RunAsync();
        }
    }
}
