using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Managers;
using LamondLu.EmailX.Infrastructure.DataPersistent;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var item = builder.Configuration.GetSection("Db");
builder.Services.Configure<DbSetting>(builder.Configuration.GetSection("Db"));
builder.Logging.AddConsole();
builder.Services.AddOptions();
builder.Services.AddScoped<IInlineImageHandler, LocalInlineImageHandler>()
                    .AddScoped<IEmailAttachmentHandler, EmailAttachmentHandler>()
                    .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                    .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                    .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                    .AddSingleton<IFileStorage, LocalFileStorage>()
                    .AddScoped<EmailConnectorManager>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
