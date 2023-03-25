using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailClient.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger _logger = null;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = null;
        private readonly IEmailConnectorWorkerFactory _emailConnectorWorkerFactory = null;
        private readonly IRuleProcessorFactory _ruleProcessorFactory = null;
        private List<EmailConnectorTask> _tasks = new List<EmailConnectorTask>();

        private IInlineImageHandler _inlineImageHandler = null;

        private IFileStorage _fileStorage = null;
        public EmailConnectorHostService()
        {
            _logger = EnvironmentConst.GetService<ILogger>();
            _unitOfWorkFactory = EnvironmentConst.GetService<IUnitOfWorkFactory>();
            _emailConnectorWorkerFactory = EnvironmentConst.GetService<IEmailConnectorWorkerFactory>();
            _ruleProcessorFactory = EnvironmentConst.GetService<IRuleProcessorFactory>();
            _inlineImageHandler = EnvironmentConst.GetService<IInlineImageHandler>();
            _fileStorage = EnvironmentConst.GetService<IFileStorage>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Write("Email Service started.");

            var unitOfWork = _unitOfWorkFactory.Create();
            var connectors = await unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            foreach (var connector in connectors)
            {
                var task = new EmailConnectorTask(connector, _emailConnectorWorkerFactory, _ruleProcessorFactory, _unitOfWorkFactory, _inlineImageHandler, _fileStorage);

                Version(connector);
                _tasks.Add(task);

                await Task.Run(async () =>
                {
                    await task.Start();
                });

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
