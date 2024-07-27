using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;

namespace LamondLu.EmailX.Infrastructure.EmailService.MailKit.Connectors
{
    public interface IEmailConnectorWorkerFactory
    {
        IEmailConnectorWorker Build(EmailConnector emailConnector,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler,IEmailAttachmentHandler emailAttachmentHandler, IEncrypt encryptor);
    }
}