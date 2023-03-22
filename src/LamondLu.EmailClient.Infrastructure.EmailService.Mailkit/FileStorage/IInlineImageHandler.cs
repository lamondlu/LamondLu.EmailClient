using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public interface IInlineImageHandler
    {
        string PopulateInlineImages(MimeMessage newMessage);
    } 
}