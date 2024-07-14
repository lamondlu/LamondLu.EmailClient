using System;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class EmailRuleQueryViewModel
    {
        public string RuleName { get; set; }

        public Guid EmailRuleId { get; set; }

        public RuleType RuleType { get; set; }
    }
}