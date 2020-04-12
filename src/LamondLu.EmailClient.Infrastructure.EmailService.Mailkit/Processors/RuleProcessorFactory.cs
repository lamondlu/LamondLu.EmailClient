using System;
using System.Collections.Generic;
using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class RuleProcessorFactory : IRuleProcessorFactory
    {
        public RuleProcessorFactory()
        {

        }

        public IRuleProcessor GetRuleProcessor(Rule rule, IUnitOfWork unitOfWork)
        {
            if (rule is ClassifyRule)
            {
                return new ClassifyRuleProcessor(unitOfWork);
            }
            else if (rule is ReplyRule)
            {
                return new ReplyRuleProcessor(unitOfWork);
            }
            else if (rule is ForwardRule)
            {
                return new ForwardRuleProcessor(unitOfWork);
            }
            else
            {
                return new NoMatchedRuleProcessor();
            }
        }
    }
}