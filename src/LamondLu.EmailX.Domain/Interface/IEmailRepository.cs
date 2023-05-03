using System;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailRepository
    {
        Task SaveNewEmail(AddEmailModel email);

        Task<bool> MessageIdExisted(string messageId);

        Task SaveEmailBody(Guid emailId, string emailBody, string emailHTMLBody);
    }
}