using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailRecipientRepository
    {
         Task SaveEmailReceipt(AddEmailRecipientModel emailRecipient);

         Task<List<EmailRecipientViewModel>> GetEmailRecipients(Guid emailId);
    }
}