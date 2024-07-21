using System;
using System.Linq;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Managers;
using LamondLu.EmailX.Infrastructure.DataPersistent;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LamondLu.EmailX.Client.Extensions
{
    public static class EnableEmailConnectorServiceExtension
    {
        public static IHostBuilder ConfigureEmailX(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.Configure<DbSetting>(hostContext.Configuration.GetSection("Db"));

                var settings = new DbSetting();
                hostContext.Configuration.Bind(settings);

                if (settings.IsValid)
                {
                    services.AddHostedService<EmailConnectorHostService>()
                                        .AddScoped<IInlineImageHandler, LocalInlineImageHandler>()
                                        .AddScoped<IEmailAttachmentHandler, EmailAttachmentHandler>()
                                        .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                                        .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                                        .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                                        .AddSingleton<IFileStorage, LocalFileStorage>();

                    services.AddScoped<EmailConnectorManager>();

                    services.AddSingleton<EmailConnectorHostService>(serviceProvider =>
                    {
                        return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
                    });

                    services.AddSingleton<IEmailConnectorAction, EmailConnectorHostService>(serviceProvider =>
                    {
                        return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
                    });

                }
                else
                {
                    Console.WriteLine("Error: The email connector is not configured properly. System won't enable it.");
                }
            });

            return builder;
        }

        public static void ConfigureEmailX(this WebApplicationBuilder? builder)
        {
            if (builder == null)
            {
                return;
            }


            builder.Services.Configure<DbSetting>(builder.Configuration.GetSection("Db"));
            var settings = new DbSetting();

            settings.ConnectionString = builder.Configuration.GetSection("Db:ConnectionString").Value;
            settings.TimeOut = Convert.ToInt32(builder.Configuration.GetSection("Db:Timeout").Value);

            builder.Configuration.Bind(settings);


            if (settings.IsValid)
            {
                builder.Services.AddHostedService<EmailConnectorHostService>()
                                    .AddSingleton<IInlineImageHandler, LocalInlineImageHandler>()
                                    .AddSingleton<IEmailAttachmentHandler, EmailAttachmentHandler>()
                                    .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                                    .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                                    .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                                    .AddSingleton<IFileStorage, LocalFileStorage>();

                builder.Services.AddScoped<EmailConnectorManager>();

                builder.Services.AddSingleton(serviceProvider =>
                {
                    return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
                });

                builder.Services.AddSingleton<IEmailConnectorAction, EmailConnectorHostService>(serviceProvider =>
                {
                    return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
                });

            }
            else
            {
                Console.WriteLine("Error: The email connector is not configured properly. System won't enable it.");
            }
        }
    }
}