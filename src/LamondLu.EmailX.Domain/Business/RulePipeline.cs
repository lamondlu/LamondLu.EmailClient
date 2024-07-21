using LamondLu.EmailX.Domain.Interface;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain
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

        public async Task Run(Email email)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Match(email))
                {
                    IRuleProcessor ruleProcessor = _factory.GetRuleProcessor(rule, _unitOfWork);
                    await ruleProcessor.Run(email, rule);

                    if (rule.StopProcessingMoreRule)
                    {
                        return;
                    }
                }
            }
        }
    }
}
