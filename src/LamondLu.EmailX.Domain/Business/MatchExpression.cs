using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using LamondLu.EmailX.Domain.Constants;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.Business
{
    public class MatchExpression
    {

        private List<string> _valueList;

        public MatchField Field { get; set; }

        public MatchCondition? Condition { get; set; }

        public MatchOperator Operator { get; set; }

        public string Value { get; set; }

        [JsonIgnore]
        public List<string> ValueList
        {
            get
            {
                if (_valueList != null)
                {
                    return _valueList;
                }

                if (string.IsNullOrWhiteSpace(Value))
                {
                    return new List<string>();
                }
                else
                {
                    return Value.Split(EmailConstantHelper.SeparatorForConditionValue).ToList();
                }
            }

        }

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
                sourceValue = email.TextBody;
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
                return source.Any(p => ValueList.Any(v => string.Compare(v, p, true) == 0));
            }
            else if (Condition == MatchCondition.NotEqual)
            {
                return source.All(p => ValueList.All(v => string.Compare(v, p, true) != 0));
            }
            else if (Condition == MatchCondition.Contain)
            {
                return source.Any(p => ValueList.Any(v => p.Contains(v, StringComparison.OrdinalIgnoreCase)));
            }
            else if (Condition == MatchCondition.NotContain)
            {
                return source.All(p => ValueList.All(v => !p.Contains(v, StringComparison.OrdinalIgnoreCase)));
            }
            else if (Condition == MatchCondition.HasAttachment)
            {
                return source.Count > 0;
            }
            else if (Condition == MatchCondition.NoAttachment)
            {
                return source.Count == 0;
            }

            return false;
        }

        private bool Compare(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (Condition == MatchCondition.Equal)
            {
                return ValueList.Any(x => string.Compare(x, source, true) == 0);
            }
            else if (Condition == MatchCondition.NotEqual)
            {
                return ValueList.All(x => string.Compare(x, source, true) != 0); 
            }
            else if (Condition == MatchCondition.Contain)
            {
                return ValueList.Any(x => source.Contains(x, StringComparison.OrdinalIgnoreCase)); 
            }
            else if (Condition == MatchCondition.NotContain)
            {
                return ValueList.All(x => !source.Contains(x, StringComparison.OrdinalIgnoreCase)); 
            }

            return false;
        }
    }
}