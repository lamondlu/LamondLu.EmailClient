using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage;

namespace LamondLu.EmailClient.Infrastructure.EmailService.MailKit.Connectors
{
    public interface IEmailConnectorWorkerFactory
    {
        IEmailConnectorWorker Build(EmailConnector emailConnector,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler, IFileStorage fileStorage);
    }
}