using System;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailRepository
    {
        Task SaveNewEmail(AddEmailModel email);

        Task<bool> MessageIdExisted(string messageId);

        Task SaveEmailBody(Guid emailId, string emailBody, string emailHTMLBody);

        Task<PagedResult<EmailListViewModel>> GetEmails(Guid emailConnectorId, int pageSize, int pageNum);

        Task<EmailDetailedViewModel> GetEmail(Guid emailId);

        Task<string> GetEmailBody(Guid emailId);
    }
}