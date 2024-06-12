namespace LamondLu.EmailX.Domain.Interface
{
    public interface IRuleProcessorFactory
    {
        IRuleProcessor GetRuleProcessor(Rule rule, IUnitOfWork unitOfWork);
    }
}