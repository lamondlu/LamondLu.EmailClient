using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Client
{
    public class EmailConnectorHostService : IHostedService
    {
        private readonly ILogger<EmailConnectorHostService> _logger = null;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = null;
        private readonly IEmailConnectorWorkerFactory _emailConnectorWorkerFactory = null;
        private readonly IRuleProcessorFactory _ruleProcessorFactory = null;
        private List<EmailConnectorTask> _tasks = new List<EmailConnectorTask>();

        private IInlineImageHandler _inlineImageHandler = null;

        private IEmailAttachmentHandler _emailAttachmentHandler = null;
        public EmailConnectorHostService(ILogger<EmailConnectorHostService> logger,
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
            _logger.LogInformation("Email Service started.");

            var unitOfWork = _unitOfWorkFactory.Create();
            var connectors = await unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            foreach (var connector in connectors.Where(p => p.IsRunning))
            {
                var task = new EmailConnectorTask(connector, _emailConnectorWorkerFactory, _ruleProcessorFactory, _unitOfWorkFactory, _inlineImageHandler, _emailAttachmentHandler);

                Version(connector);
                _tasks.Add(task);

                task.Start();
            }
        }

        private void Version(EmailConnectorConfigViewModel emailConnector)
        {
            _logger.LogInformation($"[{emailConnector.Name}] Email Connect Type: {emailConnector.Type}");
            _logger.LogInformation($"[{emailConnector.Name}] SSL: {(emailConnector.EnableSSL ? "Yes" : "No")}");
            _logger.LogInformation($"[{emailConnector.Name}] UserName: {emailConnector.UserName}");
            _logger.LogInformation($"[{emailConnector.Name}] Password: {emailConnector.Password}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation("Email Service stopped.");
                _tasks.ForEach(async p => await p.Stop());
                _tasks.Clear();
            });
        }
    }
}
