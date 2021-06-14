using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger _logger = null;
        private readonly Settings _settings = null;
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = null;
        private List<EmailConnectorTask> _tasks = new List<EmailConnectorTask>();
        private List<Thread> _threads = new List<Thread>();


        public EmailConnectorHostService()
        {
            _logger = (ILogger)EnvironmentConst.Services.GetService(typeof(ILogger));
            _settings = EnvironmentConst.EmailSettings;
            _unitOfWork = (IUnitOfWork)EnvironmentConst.Services.GetService(typeof(IUnitOfWork));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Write("Email Service started.");

            var connectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            foreach (var connector in connectors)
            {
                var task = new EmailConnectorTask(connector);

                Version(connector);
                _tasks.Add(task);

                Thread thread = new Thread(task.Start);
                thread.Start();

                _threads.Add(thread);

                _logger.Write($"[{connector.Name}] Started");
            }
        }

        private void Version(EmailConnectorConfigViewModel emailConnector)
        {
            _logger.Write($"[{emailConnector.Name}] Email Connect Type: {emailConnector.Type}");
            _logger.Write($"[{emailConnector.Name}] Address: {emailConnector.IP}");
            _logger.Write($"[{emailConnector.Name}] Port: {emailConnector.Port}");
            _logger.Write($"[{emailConnector.Name}] SSL: {(emailConnector.EnableSSL ? "Yes" : "No")}");
            _logger.Write($"[{emailConnector.Name}] UserName: {emailConnector.UserName}");
            _logger.Write($"[{emailConnector.Name}] Password: {emailConnector.Password}");
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
