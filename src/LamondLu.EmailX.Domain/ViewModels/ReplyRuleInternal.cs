using LamondLu.EmailX.Domain.Enum;
using System;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class ReplyRuleInternal
    {
        public Guid EmailRule { get; set; }

        public string RuleName { get; set; }

        public MatchField ConditionType { get; set; }

        public MatchOperator ConditionOperator { get; set; }

        public string ConditionValue { get; set; }

        public Guid EmailTemplateId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Name { get; set; }

        public bool TerminateIfMatch { get; set; }

        public int Order { get; set; }
    }
}
