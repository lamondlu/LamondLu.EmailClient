using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Client
{
    public class EmailConnectorHostService : IHostedService, IEmailConnectorAction
    {
        private readonly ILogger<EmailConnectorHostService> _logger = null;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory = null;
        private readonly IEmailConnectorWorkerFactory _emailConnectorWorkerFactory = null;
        private readonly IRuleProcessorFactory _ruleProcessorFactory = null;
        private static List<EmailConnectorTask> _tasks = new List<EmailConnectorTask>();

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
            var runningConnectorIds = await unitOfWork.EmailConnectorRepository.GetAllRunningEmailConnectorIds();

            foreach (var connectorId in runningConnectorIds)
            {
                var connector = await unitOfWork.EmailConnectorRepository.GetEmailConnector(connectorId);

                var task = new EmailConnectorTask(connector, _emailConnectorWorkerFactory, _ruleProcessorFactory, _unitOfWorkFactory, _inlineImageHandler, _emailAttachmentHandler, _logger);

                _tasks.Add(task);
                task.Start();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation("Email Connector stopped.");
                _tasks.ForEach(async p => await p.Stop());
                _tasks.Clear();
            });
        }

        public async Task StopConnector(Guid emailConnectorId)
        {
            await _tasks.FirstOrDefault(p => p.EmailConnectorId == emailConnectorId)?.Stop();
            _tasks.RemoveAll(p => p.EmailConnectorId == emailConnectorId);
            _logger.LogInformation($"Email Connector {emailConnectorId} stopped.");
        }

        public async Task StartConnector(Guid emailConnectorId)
        {
            var unitOfWork = _unitOfWorkFactory.Create();
            var connector = await unitOfWork.EmailConnectorRepository.GetEmailConnector(emailConnectorId);

            if (connector == null)
            {
                _logger.LogError($"Email Connector {emailConnectorId} not found.");
                return;
            }
            else
            {
                var task = new EmailConnectorTask(connector, _emailConnectorWorkerFactory, _ruleProcessorFactory, _unitOfWorkFactory, _inlineImageHandler, _emailAttachmentHandler, _logger);

                _tasks.Add(task);

                task.Start();
            }
        }
    }
}
