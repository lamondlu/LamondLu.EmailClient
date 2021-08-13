using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Extension;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorWorkFactory : IEmailConnectorWorkerFactory
    {
        public IEmailConnectorWorker Build(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            if (emailConnector.Type.IsPop3())
            {
                return new POP3EmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork);
            }
            else if (emailConnector.Type.IsIMAP())
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork);
            }
            else
            {
                return new IMAPEmailConnectorWorker(emailConnector, ruleProcessorFactory, unitOfWork);
            }
        }
    }
}