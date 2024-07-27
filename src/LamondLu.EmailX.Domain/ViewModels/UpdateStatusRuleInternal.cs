using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class UpdateStatusRuleInternal
    {
        public string RuleName { get; set; }
        public MatchField ConditionType { get; set; }

        public MatchCondition ConditionOperator { get; set; }

        public string ConditionValue { get; set; }

        public EmailStatus EmailStatus { get; set; }

        public bool TerminateIfMatch { get; set; }

        public int Order { get; set; }
    }
}
