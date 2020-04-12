using System;
using System.Collections.Generic;
using System.Text;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Domain
{
    public class RulePipeline
    {
        private List<Rule> _rules = null;
        private IRuleProcessorFactory _factory = null;

        private IUnitOfWork _unitOfWork = null;

        public RulePipeline(List<Rule> rules, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            _rules = rules;
            _factory = ruleProcessorFactory;
            _unitOfWork = unitOfWork;
        }

        public void Run(Email email)
        {
            foreach (var rule in _rules)
            {
                if (rule.Match(email))
                {
                    //process the handler

                    var ruleProcessor = _factory.GetRuleProcessor(rule, _unitOfWork);
                    ruleProcessor.Run(email);

                    if (rule.TerminateIfMatch)
                    {
                        return;
                    }
                }
            }
        }
    }
}
