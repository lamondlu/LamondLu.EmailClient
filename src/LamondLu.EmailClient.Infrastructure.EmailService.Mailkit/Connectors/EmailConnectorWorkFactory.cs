using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Extension;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage;
using LamondLu.EmailClient.Infrastructure.EmailService.MailKit.Connectors;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorWorkFactory : IEmailConnectorWorkerFactory
    {
        public IEmailConnectorWorker Build(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler, IFileStorage fileStorage)
        {
            if (emailConnector.Type.IsPop3())
            {
                return new POP3EmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork);
            }
            else if (emailConnector.Type.IsIMAP())
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork, inlineImageHandler, fileStorage);
            }
            else
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork, inlineImageHandler, fileStorage);
            }
        }
    }
}