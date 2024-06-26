using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    /// <summary>
    /// A interface to handle email attachments
    /// </summary>
    public interface IEmailAttachmentHandler
    {
        Task<List<AddEmailAttachmentModel>> SaveAttachment(Guid emailId, MimeMessage mail);
    }
}