using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger _logger = null;
        private readonly Settings _settings = null;
        private readonly IUnitOfWork _unitOfWork = null;

        public EmailConnectorHostService()
        {
            _logger = (ILogger)EnvironmentConst.Services.GetService(typeof(ILogger));
            _settings = EnvironmentConst.EmailSettings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Write("Email Service started.");

            var connectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            foreach (var connector in connectors)
            {
                Version(connector);
            }
        }

        private void Version(EmailConnectorConfigViewModel emailConnector)
        {
            _logger.Write($"Email Connect Type: {emailConnector.Type}");
            _logger.Write($"Address: {emailConnector.IP}");
            _logger.Write($"Port: {emailConnector.Port}");
            _logger.Write($"SSL: {(emailConnector.EnableSSL ? "Yes" : "No")}");
            _logger.Write($"UserName: {emailConnector.UserName}");
            _logger.Write($"Password: {emailConnector.Password}");
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
