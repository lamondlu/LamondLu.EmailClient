using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class RuleProcessorFactory : IRuleProcessorFactory
    {
        public RuleProcessorFactory()
        {

        }

        public IRuleProcessor GetRuleProcessor(Rule rule, IUnitOfWork unitOfWork)
        {
            if (rule is ClassifyRule)
            {
                return new ClassifyRuleProcessor(unitOfWork);
            }
            else if (rule is ReplyRule)
            {
                return new ReplyRuleProcessor(unitOfWork);
            }
            else if (rule is ForwardRule)
            {
                return new ForwardRuleProcessor(unitOfWork);
            }
            else
            {
                return new NoMatchedRuleProcessor();
            }
        }
    }
}