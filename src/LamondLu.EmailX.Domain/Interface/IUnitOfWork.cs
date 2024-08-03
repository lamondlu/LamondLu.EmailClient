using LamondLu.EmailX.Domain.Models;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IUnitOfWork
    {
        IEmailConnectorRepository EmailConnectorRepository { get; }

        IEmailFolderRepository EmailFolderRepository { get; }

        IEmailRepository EmailRepository { get; }

        IEmailAttachmentRepository EmailAttachmentRepository { get; }

        IEmailRecipientRepository EmailRecipientRepository { get; }

        IEmailTagRepository EmailTagRepository { get; }

        Task<DbOperationResult> SaveAsync();

        void CreateDatabaseIfNotExisted();
    }
}
