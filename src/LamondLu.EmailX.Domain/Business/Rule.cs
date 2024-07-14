using LamondLu.EmailX.Domain.Business;
using LamondLu.EmailX.Domain.Enum;
using System;

namespace LamondLu.EmailX.Domain
{
    public class Rule
    {
        public Rule()
        {
            EmailRuleId = Guid.NewGuid();
        }

        public Rule(RuleType ruleType)
        {
            RuleType = ruleType;
        }

        public string RuleName { get; set; }
        public Guid EmailRuleId { get; set; }

        public RuleType RuleType { get; private set; }

        public MatchExpression Expression { get; set; }

        public MatchExpressionCollection Expressions { get; set; }

        public bool StopProcessingMoreRule { get; set; }
        
        public int Order { get; set; }

        public virtual bool Match(Email email)
        {
            return Expressions.IsMatch(email);
        }
    }
}
