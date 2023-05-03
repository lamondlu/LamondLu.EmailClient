using LamondLu.EmailX.Domain.Business;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class Email
    {
        public Email(string messageId, uint mailkitId, uint mailkitValidityId)
        {
            EmailId = new EmailId(messageId, mailkitId, mailkitValidityId);
            Tags = new List<Tag>();
            Attachments = new List<EmailAttachment>();
        }

        public EmailId EmailId { get; private set; }

        public EmailFolder EmailFolder { get; set; }

        public uint EmailRealId { get; set; }

        public uint EmailValidityId { get; set; }

        public string MessageId { get; set; }

        public DateTime ReceivedDate { get; set; }

        public EmailAddress Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string TextBody { get; set; }

        public string Notes { get; set; }

        public List<EmailAddress> Receipts { get; set; }

        public List<EmailAddress> CCs { get; set; }

        public List<EmailAddress> BCCs { get; set; }

        public List<EmailAddress> ReplyTos { get; set; }

        public List<EmailAttachment> Attachments { get; set; }

        public List<Tag> Tags { get; set; }

        public bool IsRead {get;set;}
    }
}
