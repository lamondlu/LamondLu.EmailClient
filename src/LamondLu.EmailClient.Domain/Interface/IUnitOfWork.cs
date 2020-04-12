using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
