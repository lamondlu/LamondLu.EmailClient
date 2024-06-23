using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{

    /// <summary>
    /// A interface to handle inline images in email
    /// </summary>
    public interface IInlineImageHandler
    {
        string PopulateInlineImages(MimeMessage newMessage, Guid emailId);
    } 
}