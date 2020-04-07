using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain
{
    public class RulePipeline
    {
        private List<Rule> _rules = null;

        public RulePipeline(List<Rule> rules)
        {
            _rules = rules;
        }

        public void Run(Email email)
        {
            foreach (var rule in _rules)
            {
                if (rule.Match(email))
                {
                    //process the handler

                    if (rule.TerminateIfMatch)
                    {
                        return;
                    }
                }
            }
        }
    }
}
