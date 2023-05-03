using LamondLu.EmailX.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LamondLu.EmailX.Domain
{
    public class MatchCritera
    {
        public MatchField Field { get; set; }

        public MatchOperator? Operator { get; set; }

        public MatchCondition Condition { get; set; }

        public string Value { get; set; }

        public bool IsMatch(Email email)
        {
            bool result = false;
            string sourceValue = string.Empty;

            if (Field == MatchField.Subject)
            {
                sourceValue = email.Subject;
                result = Compare(sourceValue);
            }
            else if (Field == MatchField.Body)
            {
                sourceValue = email.Body;
                result = Compare(sourceValue);
            }
            else if (Field == MatchField.Sender)
            {
                sourceValue = email.Sender.Address;
                result = Compare(sourceValue);
            }
            else if (Field == MatchField.Attachment)
            {
                result = Compare(email.Attachments.Select(p => p.FileName).ToList());
            }

            return result;
        }

        private bool Compare(List<string> source)
        {
            if (Condition == MatchCondition.Equal)
            {
                return source.Any(p => string.Compare(p, Value, true) == 0);
            }
            else if (Condition == MatchCondition.NotEqual)
            {
                return source.All(p => string.Compare(p, Value, true) != 0);
            }
            else if (Condition == MatchCondition.Contain)
            {
                return source.Any(p => p.Contains(Value, StringComparison.OrdinalIgnoreCase));
            }
            else if (Condition == MatchCondition.NotContain)
            {
                return source.All(p => !p.Contains(Value, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }

        private bool Compare(string source)
        {
            if (Condition == MatchCondition.Equal)
            {
                return string.Compare(source, Value, true) == 0;
            }
            else if (Condition == MatchCondition.NotEqual)
            {
                return string.Compare(source, Value, true) != 0;
            }
            else if (Condition == MatchCondition.Contain)
            {
                return source.Contains(Value, StringComparison.OrdinalIgnoreCase);
            }
            else if (Condition == MatchCondition.NotContain)
            {
                return !source.Contains(Value, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
