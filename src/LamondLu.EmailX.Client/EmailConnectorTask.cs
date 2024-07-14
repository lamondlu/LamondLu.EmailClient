using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Client
{
    public class EmailConnectorTask
    {
        private EmailConnectorConfigViewModel _emailConnector = null;

        private IEmailConnectorWorkerFactory _factory = null;

        private IRuleProcessorFactory _ruleProcessorFactory = null;

        private IUnitOfWorkFactory _unitOfWorkFactory = null;

        private IInlineImageHandler _inlineImageHandler = null;

        private IEmailAttachmentHandler _emailAttachmentHandler = null;

        private IEmailConnectorWorker _emailConnectorWorker = null;

        private ILogger _logger = null;

        public EmailConnectorTask(EmailConnectorConfigViewModel emailConnector, IEmailConnectorWorkerFactory factory, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWorkFactory unitOfWorkFactory, IInlineImageHandler inlineImageHandler, IEmailAttachmentHandler emailAttachmentHandler, ILogger<EmailConnectorHostService> logger)
        {
            _emailConnector = emailConnector;
            _factory = factory;
            _ruleProcessorFactory = ruleProcessorFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _inlineImageHandler = inlineImageHandler;
            _emailAttachmentHandler = emailAttachmentHandler;
            _logger = logger;
        }

        public Guid EmailConnectorId => _emailConnector.EmailConnectorId;

        public async Task Start()
        {
            await ConnectAsync();
        }

        public async Task Stop()
        {
            _logger.LogInformation($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Stop");
            await _emailConnectorWorker.Disconnect();
        }

        public async Task ConnectAsync()
        {
            _logger.LogInformation($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Start");

            EmailConnector emailConnector = new EmailConnector(_emailConnector.EmailConnectorId, _emailConnector.Name, _emailConnector.EmailAddress, _emailConnector.UserName, _emailConnector.Password, new EmailServerConfig(_emailConnector.SMTPServer, _emailConnector.SMTPPort, _emailConnector.IMAPServer, _emailConnector.IMAPPort, _emailConnector.POP3Server, _emailConnector.POP3Port, _emailConnector.EnableSSL)
            , _emailConnector.Type, string.Empty);

            try
            {
                _emailConnectorWorker = _factory.Build(emailConnector, _ruleProcessorFactory, _unitOfWorkFactory.Create(), _inlineImageHandler, _emailAttachmentHandler);

                var isConnected = await _emailConnectorWorker.Connect();

                if (isConnected)
                {
                    _logger.LogInformation($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Connected");
                    await _emailConnectorWorker.Listen();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Connect Error");
            }
        }
    }
}
