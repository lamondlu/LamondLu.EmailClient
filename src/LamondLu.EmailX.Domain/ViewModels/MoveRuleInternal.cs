
using System;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class MoveRuleInternal
    {
        public string RuleName { get; set; }
        public MatchField ConditionType { get; set; }

        public MatchCondition ConditionOperator { get; set; }

        public string ConditionValue { get; set; }

        public Guid EmailConnectorId { get; set; }
        public Guid? EmailFolderId { get; set; }
        public bool TerminateIfMatch { get; set; }
        public int Order { get; set; }
    }
}
