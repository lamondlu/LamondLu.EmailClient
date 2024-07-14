using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailAttachmentRepository
    {

        Task<List<EmailAttachmentViewModel>> GetEmailAttachments(Guid emailId);
    }
}