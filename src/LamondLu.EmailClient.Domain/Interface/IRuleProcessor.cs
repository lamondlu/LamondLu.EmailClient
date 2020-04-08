namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IRuleProcessor
    {
        void Run(Email email);
    }
}