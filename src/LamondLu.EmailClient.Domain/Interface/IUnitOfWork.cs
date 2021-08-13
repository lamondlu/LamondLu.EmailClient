using LamondLu.EmailClient.Domain.Models;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IUnitOfWork
    {
        IEmailConnectorRepository EmailConnectorRepository { get; }

        IEmailFolderRepository EmailFolderRepository { get; }

        Task<DbOperationResult> SaveAsync();
    }
}
