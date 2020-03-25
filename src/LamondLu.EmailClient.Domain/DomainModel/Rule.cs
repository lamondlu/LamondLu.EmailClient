using LamondLu.EmailClient.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DomainModel
{
    public class Rule
    {
        public Guid RuleId { get; private set; }

        public RuleType RuleType { get; private set; }

        public MatchCritera Critera { get; set; }

        public bool TerminateIfMatch { get; set; }

        public virtual bool Match(Email email)
        {
            return Critera.IsMatch(email);
        }
    }
}
