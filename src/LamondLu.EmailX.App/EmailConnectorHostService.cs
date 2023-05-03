using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailX.App
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger _logger = null;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = null;
        private readonly IEmailConnectorWorkerFactory _emailConnectorWorkerFactory = null;
        private readonly IRuleProcessorFactory _ruleProcessorFactory = null;
        private List<EmailConnectorTask> _tasks = new List<EmailConnectorTask>();

        private IInlineImageHandler _inlineImageHandler = null;

        private IEmailAttachmentHandler _emailAttachmentHandler = null;
        public EmailConnectorHostService(ILogger logger,
        IUnitOfWorkFactory unitOfWorkFactory,
          IEmailConnectorWorkerFactory emailConnectorWorkerFactory,
          IRuleProcessorFactory ruleProcessorFactory,
          IInlineImageHandler inlineImageHandler,
          IEmailAttachmentHandler emailAttachmentHandler)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailConnectorWorkerFactory = emailConnectorWorkerFactory;
            _ruleProcessorFactory = ruleProcessorFactory;
            _inlineImageHandler = inlineImageHandler;
            _emailAttachmentHandler = emailAttachmentHandler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Write("Email Service started.");

            var unitOfWork = _unitOfWorkFactory.Create();
            var connectors = await unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            foreach (var connector in connectors)
            {
                var task = new EmailConnectorTask(connector, _emailConnectorWorkerFactory, _ruleProcessorFactory, _unitOfWorkFactory, _inlineImageHandler, _emailAttachmentHandler);

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
