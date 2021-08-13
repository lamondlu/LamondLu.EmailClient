using LamondLu.EmailClient.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace LamondLu.EmailClient.Domain
{
    public class MatchCriteraCollection
    {
        public MatchCriteraCollection(List<MatchCritera> criteras)
        {
            BuildMatchGroup(criteras);
        }

        public List<MatchCritera> Criteras { get; set; }

        private List<List<MatchCritera>> _matchGroups = new List<List<MatchCritera>>();

        private void BuildMatchGroup(List<MatchCritera> matchExpressions)
        {
            List<MatchCritera> currentGroup = null;

            var count = matchExpressions.Count;
            var current = 0;
            foreach (var item in matchExpressions)
            {
                current++;
                if (!item.Operator.HasValue || item.Operator.Value == MatchOperator.AND)
                {
                    if (currentGroup == null)
                    {
                        currentGroup = new List<MatchCritera>();
                    }

                    currentGroup.Add(item);
                }
                else
                {
                    _matchGroups.Add(currentGroup);
                    currentGroup = new List<MatchCritera>();
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
