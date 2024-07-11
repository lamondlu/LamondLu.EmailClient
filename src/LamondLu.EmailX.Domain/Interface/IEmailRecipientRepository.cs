using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailRecipientRepository
    {
         Task SaveEmailReceipt(AddEmailRecipientModel emailRecipient);
    }
}