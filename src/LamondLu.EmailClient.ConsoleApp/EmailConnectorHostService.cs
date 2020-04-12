using LamondLu.EmailClient.Domain.Interface;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger _logger = null;
        private readonly Settings _settings = null;

        public EmailConnectorHostService()
        {
            _logger = (ILogger)EnvironmentConst.Services.GetService(typeof(ILogger));
            _settings = EnvironmentConst.EmailSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.Write("Email Service started.");

                Version();
            });
        }

        private void Version()
        {
            _logger.Write($"Email Connect Type: {_settings.Type}");
            _logger.Write($"Address: {_settings.IP}");
            _logger.Write($"Port: {_settings.Port}");
            _logger.Write($"SSL: {(_settings.EnableSSL ? "Yes" : "No")}");
            _logger.Write($"UserName: {_settings.UserName}");
            _logger.Write($"Password: {_settings.Password}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.Write("Email Service stopped.");
            });
        }
    }
}
