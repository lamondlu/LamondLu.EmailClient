using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailRepository
    {
        Task SaveNewEmail(AddEmailModel email);

        Task<bool> MessageIdExisted(string messageId);
    }
}