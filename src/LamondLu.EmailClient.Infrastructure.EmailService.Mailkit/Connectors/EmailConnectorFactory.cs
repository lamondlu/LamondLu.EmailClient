using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Extension;
using LamondLu.EmailClient.Domain.Interface;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorFactory : IEmailConnectorFactory
    {
        public IEmailConnector Build(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            if (emailConnector.Type.IsPop3())
            {
                return new POP3EmailConnector(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            }
            else if (emailConnector.Type.IsIMAP())
            {
                return new IMAPEmailConnector(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            }
            else
            {
                return new IMAPEmailConnector(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            }
        }
    }
}