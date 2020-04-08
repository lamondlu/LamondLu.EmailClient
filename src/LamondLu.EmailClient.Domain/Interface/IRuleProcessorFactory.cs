namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IRuleProcessorFactory
    {
        IRuleProcessor GetRuleProcessor(Rule rule);
    }
}