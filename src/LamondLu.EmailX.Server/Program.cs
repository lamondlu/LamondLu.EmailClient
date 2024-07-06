using LamondLu.EmailX.Client;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Managers;
using LamondLu.EmailX.Infrastructure.DataPersistent;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using MimeKit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var item = builder.Configuration.GetSection("Db");
builder.Services.Configure<DbSetting>(builder.Configuration.GetSection("Db"));
builder.Logging.AddConsole();
builder.Services.AddOptions();
builder.Services.AddSingleton<IInlineImageHandler, LocalInlineImageHandler>()
                    .AddSingleton<IEmailAttachmentHandler, EmailAttachmentHandler>()
                    .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                    .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                    .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                    .AddSingleton<IFileStorage, LocalFileStorage>();



builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<EmailConnectorHostService>();
builder.Services.AddSingleton<EmailConnectorHostService>(serviceProvider =>
{
    return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
});
builder.Services.AddScoped<EmailConnectorManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();
app.MapControllers();

app.Run();
