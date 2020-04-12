using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Extension;
using LamondLu.EmailClient.Domain.Interface;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorFactory : IEmailConnectorFactory
    {
        public IEmailConnector Build(EmailConnectorType emailConnectorType, List<Rule> rules, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            if (emailConnectorType.IsPop3())
            {
                return new POP3EmailConnector(rules, ruleProcessorFactory, unitOfWork);
            }
            else if (emailConnectorType.IsIMAP())
            {
                return new IMAPEmailConnector(rules, ruleProcessorFactory, unitOfWork);
            }
            else
            {
                return new IMAPEmailConnector(rules, ruleProcessorFactory, unitOfWork);
            }
        }
    }
}