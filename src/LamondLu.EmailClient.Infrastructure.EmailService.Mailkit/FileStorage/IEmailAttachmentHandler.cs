using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;
using MimeKit;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public interface IEmailAttachmentHandler
    {
        Task<List<AddEmailAttachmentModel>> SaveAttachment(Guid emailId, MimeMessage mail);
    }
}