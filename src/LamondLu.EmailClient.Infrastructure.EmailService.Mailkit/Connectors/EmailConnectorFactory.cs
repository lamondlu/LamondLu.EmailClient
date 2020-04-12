using System.Collections.Generic;
using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Extension;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorFactory : IEmailConnectorFactory
    {
        public IEmailConnector Build(EmailConnectorType emailConnectorType, List<Rule> rules)
        {
            if (emailConnectorType.IsPop3())
            {
                return new POP3EmailConnector(rules);
            }
            else if (emailConnectorType.IsIMAP())
            {
                return new IMAPEmailConnector(rules);
            }
            else
            {
                return new IMAPEmailConnector(rules);
            }
        }
    }
}