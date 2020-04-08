using System;
using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class RuleProcessorFactory : IRuleProcessorFactory
    {
        public IRuleProcessor GetRuleProcessor(Rule rule)
        {
            throw new NotImplementedException();
        }
    }
}