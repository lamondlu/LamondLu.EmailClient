using System;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.ViewModels.Emails
{
    public class EmailDetailedViewModel
    {
        public Guid EmailId { get; set; }

        public string Subject { get; set; }

        public string Sender { get; set; }

        public string From { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string EmailHTMLBody { get; set; }
    }

    public class EmailRecipientViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public EmailRecipientType Type { get; set; }
    }

    public class EmailAttachmentViewModel
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string SourceFileName{get;set;}
    }
}