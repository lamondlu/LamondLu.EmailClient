using System.Collections.Generic;
using System.Linq;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.Business
{
    public class MatchExpressionCollection
    {
        public MatchExpressionCollection(List<MatchExpression> matchExpressions)
        {
            BuildMatchGroup(matchExpressions);
        }

        private List<List<MatchExpression>> _matchGroups = new List<List<MatchExpression>>();

        private void BuildMatchGroup(List<MatchExpression> matchExpressions)
        {
            List<MatchExpression> currentGroup = null;

            var count = matchExpressions.Count;
            var current = 0;
            foreach (var item in matchExpressions)
            {
                current++;
                if (!item.Condition.HasValue || item.Condition == MatchCondition.AND)
                {
                    if (currentGroup == null)
                    {
                        currentGroup = new List<MatchExpression>();
                    }

                    currentGroup.Add(item);
                }
                else
                {
                    _matchGroups.Add(currentGroup);
                    currentGroup = new List<MatchExpression>();
                    currentGroup.Add(item);
                }

                if (current == count)
                {
                    _matchGroups.Add(currentGroup);
                }
            }
        }

        public bool IsMatch(Email email)
        {
            foreach (var item in _matchGroups)
            {
                if (item.All(p => p.IsMatch(email)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}