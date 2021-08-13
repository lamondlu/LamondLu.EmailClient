namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
