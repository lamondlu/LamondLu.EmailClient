using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;
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

        private Task CurrentTask = null;

        public EmailConnectorTask(EmailConnectorConfigViewModel emailConnector, IEmailConnectorWorkerFactory factory, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWorkFactory unitOfWorkFactory, IInlineImageHandler inlineImageHandler, IEmailAttachmentHandler emailAttachmentHandler)
        {
            _emailConnector = emailConnector;
            _factory = factory;
            _ruleProcessorFactory = ruleProcessorFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _inlineImageHandler = inlineImageHandler;
            _emailAttachmentHandler = emailAttachmentHandler;
        }

        public void Start()
        {
            CurrentTask = Task.Run(async () => await ConnectAsync());
        }

        public async Task ConnectAsync()
        {
            Console.WriteLine($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Start");

            EmailConnector emailConnector = new EmailConnector(_emailConnector.EmailConnectorId, _emailConnector.Name, _emailConnector.EmailAddress, _emailConnector.UserName, _emailConnector.Password, new EmailServerConfig(_emailConnector.SMTPServer, _emailConnector.SMTPPort, _emailConnector.IMAPServer, _emailConnector.IMAPPort, _emailConnector.POP3Server, _emailConnector.POP3Port, _emailConnector.EnableSSL)
            , _emailConnector.Type, string.Empty);

            try
            {
                var worker = _factory.Build(emailConnector, _ruleProcessorFactory, _unitOfWorkFactory.Create(), _inlineImageHandler, _emailAttachmentHandler);

                var isConnected = await worker.Connect();

                if (isConnected)
                {
                    Console.WriteLine("Email Connector connected.");
                    await worker.Listen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
