using LamondLu.EmailX.Client;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Managers;
using LamondLu.EmailX.Infrastructure.DataPersistent;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var item = builder.Configuration.GetSection("Db");
builder.Services.Configure<DbSetting>(item);
builder.Logging.AddConsole();
builder.Services.AddOptions();
builder.Services.AddSingleton<IInlineImageHandler, LocalInlineImageHandler>()
                    .AddSingleton<IEmailAttachmentHandler, EmailAttachmentHandler>()
                    .AddSingleton<IEmailConnectorWorkerFactory, EmailConnectorWorkFactory>()
                    .AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
                    .AddSingleton<IRuleProcessorFactory, RuleProcessorFactory>()
                    .AddSingleton<IFileStorage, LocalFileStorage>();

builder.Services.AddHostedService<EmailConnectorHostService>();
builder.Services.AddSingleton<EmailConnectorHostService>(serviceProvider =>
{
    return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
});

builder.Services.AddSingleton<IEmailConnectorAction, EmailConnectorHostService>(serviceProvider =>
{
    return serviceProvider.GetServices<IHostedService>().Where(e => e.GetType() == typeof(EmailConnectorHostService)).Cast<EmailConnectorHostService>().Single();
});

builder.Services.AddScoped<EmailConnectorManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
