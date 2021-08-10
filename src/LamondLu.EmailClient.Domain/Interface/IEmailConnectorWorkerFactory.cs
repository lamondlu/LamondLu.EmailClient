using LamondLu.EmailClient.Domain.Enum;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorWorkerFactory
    {
        IEmailConnectorWorker Build(EmailConnector emailConnector,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork);
    }
}