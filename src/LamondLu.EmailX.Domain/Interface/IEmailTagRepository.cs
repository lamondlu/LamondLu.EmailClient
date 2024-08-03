using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailTagRepository
    {
        Task AddTagToEmail(Guid emailId, Tag tag);

        Task RemoveTagFromEmail(Guid emailId, Tag tag)
;    }
}