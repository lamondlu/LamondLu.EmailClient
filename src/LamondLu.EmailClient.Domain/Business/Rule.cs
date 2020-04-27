﻿using LamondLu.EmailClient.Domain.Enum;
using System;

namespace LamondLu.EmailClient.Domain
{
    public class Rule
    {
        public Guid RuleId { get; private set; }

        public RuleType RuleType { get; private set; }

        public MatchCriteraCollection Criteras { get; set; }

        public bool TerminateIfMatch { get; set; }

        public virtual bool Match(Email email)
        {
            return Criteras.IsMatch(email);
        }
    }
}
