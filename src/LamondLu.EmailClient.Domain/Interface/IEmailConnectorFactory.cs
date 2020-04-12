using LamondLu.EmailClient.Domain.Enum;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorFactory
    {
        IEmailConnector Build(EmailConnectorType emailConnectorType,
            List<Rule> rules,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork);
    }
}