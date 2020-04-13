using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IUnitOfWork
    {
        IEmailConnectorRepository EmailConnectorRepository { get; }

        Task SaveAsync();
    }
}
