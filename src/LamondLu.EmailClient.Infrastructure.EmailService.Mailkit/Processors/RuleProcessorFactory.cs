using System;
using System.Collections.Generic;
using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class RuleProcessorFactory : IRuleProcessorFactory
    {
        private static Dictionary<RuleType, IRuleProcessor> _processMappings = new Dictionary<RuleType, IRuleProcessor> {
            { RuleType.Classify, new ClassifyRuleProcessor() },
            { RuleType.Reply, new ReplyRuleProcessor() },
            { RuleType.Forward, new ForwardRuleProcessor() }
        };

        public RuleProcessorFactory()
        {

        }

        public IRuleProcessor GetRuleProcessor(Rule rule)
        {
            if (_processMappings.ContainsKey(rule.RuleType))
            {
                return _processMappings[rule.RuleType];
            }
            else
            {
                throw new Exception("The processor for this type is missing.");
            }
        }
    }
}