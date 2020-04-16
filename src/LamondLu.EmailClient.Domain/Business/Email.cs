using LamondLu.EmailClient.Domain.Business;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain
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

        public EmailFolder EmailFolder { get; private set; }

        public DateTime ReceivedDate { get; set; }

        public EmailAddress Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Notes { get; set; }

        public EmailAddress Receipt { get; set; }

        public List<EmailAttachment> Attachments { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
