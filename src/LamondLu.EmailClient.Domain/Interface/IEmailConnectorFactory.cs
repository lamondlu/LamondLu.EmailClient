using LamondLu.EmailClient.Domain.Enum;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorFactory
    {
        IEmailConnector Build(EmailConnector emailConnector,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork);
    }
}