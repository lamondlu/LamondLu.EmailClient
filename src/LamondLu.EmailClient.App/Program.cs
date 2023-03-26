using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.DataPersistent;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailClient.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.App
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


                var item = hostContext.Configuration.GetSection("Db");

                services.Configure<DbSetting>(hostContext.Configuration.GetSection("Db"));

                services.AddOptions();
                
                services.AddHostedService<EmailConnectorHostService>()
                    .AddScoped<IInlineImageHandler, LocalInlineImageHandler>()
                    .AddSingleton<ILogger, ConsoleLogger>()
                    .AddScoped<IEmailAttachmentHandler, EmailAttachmentHandler>()
                    .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                    .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                    .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                    .AddSingleton<IFileStorage, LocalFileStorage>();

            }).UseConsoleLifetime().Build();

            await host.RunAsync();
        }
    }
}
