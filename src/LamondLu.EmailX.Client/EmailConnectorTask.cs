using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Client
{
    public class EmailConnectorTask
    {
        private EmailConnector _emailConnector = null;

        private IEmailConnectorWorkerFactory _factory = null;

        private IRuleProcessorFactory _ruleProcessorFactory = null;

        private IUnitOfWorkFactory _unitOfWorkFactory = null;

        private IInlineImageHandler _inlineImageHandler = null;

        private IEmailAttachmentHandler _emailAttachmentHandler = null;

        private IEmailConnectorWorker _emailConnectorWorker = null;

        private ILogger _logger = null;

        private IEncrypt _encryptor = null;

        public EmailConnectorTask(EmailConnector emailConnector, IEmailConnectorWorkerFactory factory, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWorkFactory unitOfWorkFactory, IInlineImageHandler inlineImageHandler, IEmailAttachmentHandler emailAttachmentHandler, 
        IEncrypt encryptor,ILogger<EmailConnectorHostService> logger)
        {
            _emailConnector = emailConnector;
            _factory = factory;
            _ruleProcessorFactory = ruleProcessorFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _inlineImageHandler = inlineImageHandler;
            _emailAttachmentHandler = emailAttachmentHandler;
            _logger = logger;
            _encryptor = encryptor;
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

            try
            {
                _emailConnectorWorker = _factory.Build(_emailConnector, _ruleProcessorFactory, _unitOfWorkFactory.Create(), _inlineImageHandler, _emailAttachmentHandler, _encryptor);

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
