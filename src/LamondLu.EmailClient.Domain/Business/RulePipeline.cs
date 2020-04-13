using LamondLu.EmailClient.Domain.Interface;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain
{
    public class RulePipeline
    {
        private readonly List<Rule> _rules = null;
        private readonly IRuleProcessorFactory _factory = null;

        private readonly IUnitOfWork _unitOfWork = null;

        public RulePipeline(List<Rule> rules, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            _rules = rules;
            _factory = ruleProcessorFactory;
            _unitOfWork = unitOfWork;
        }

        public void Run(Email email)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Match(email))
                {
                    //process the handler

                    IRuleProcessor ruleProcessor = _factory.GetRuleProcessor(rule, _unitOfWork);
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
