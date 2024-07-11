using System;
using System.Collections.Generic;

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

        public List<EmailRecipientViewModel> To { get; set; }

        public List<EmailRecipientViewModel> Cc { get; set; }

        public List<EmailRecipientViewModel> Bcc { get; set; }

        public List<EmailRecipientViewModel> ReplyTo { get; set; }
    }

    public class EmailRecipientViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        //public EmailRecipientType Type { get; set; }
    }
}