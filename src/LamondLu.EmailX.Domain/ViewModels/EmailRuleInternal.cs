
using System;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.ViewModels
{
    internal class EmailRuleInternal
    {
        public string RuleName { get; set; }

        public Guid EmailRuleId { get; set; }

        public RuleType RuleType { get; set; }
    }
}
