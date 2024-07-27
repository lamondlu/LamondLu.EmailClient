using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Extension;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorWorkFactory : IEmailConnectorWorkerFactory
    {
        public IEmailConnectorWorker Build(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler, IEmailAttachmentHandler emailAttachmentHandler, IEncrypt encryptor)
        {
            if (emailConnector.Type.IsPop3())
            {
                return new POP3EmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork, encryptor);
            }
            else if (emailConnector.Type.IsIMAP())
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork, inlineImageHandler, emailAttachmentHandler, encryptor);
            }
            else
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork, inlineImageHandler, emailAttachmentHandler, encryptor);
            }
        }
    }
}