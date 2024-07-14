using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.DTOs;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    /// <summary>
    /// A interface to handle email attachments
    /// </summary>
    public interface IEmailAttachmentHandler
    {
        Task<List<EmailAttachment>> SaveAttachments(Guid emailId, MimeMessage mail);

        Task<Stream> DownloadAttachment(Guid emailId, string fileName);
    }
}