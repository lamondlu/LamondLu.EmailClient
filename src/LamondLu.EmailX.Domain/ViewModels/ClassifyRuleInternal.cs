using LamondLu.EmailX.Domain.Enum;
using System;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class ClassifyRuleInternal
    {
        public string RuleName { get; set; }
        public MatchField ConditionType { get; set; }

        public MatchCondition ConditionOperator { get; set; }

        public string ConditionValue { get; set; }

        public string TagName { get; set; }

        public Guid TagId { get; set; }

        public bool TerminateIfMatch { get; set; }
        public bool IsAIOCRCriteria { get; set; }
        public int Order { get; set; }
    }
}
