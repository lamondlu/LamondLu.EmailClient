using LamondLu.EmailX.Domain.Business;
using LamondLu.EmailX.Domain.Extension;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class Email
    {
        public Email(string messageId, uint mailkitId, uint mailkitValidityId)
        {
            EmailId = Guid.NewGuid();
            MessageId = messageId;
            EmailRealId = mailkitId;
            EmailValidityId = mailkitValidityId;
            Tags = new List<Tag>();
            Attachments = new List<EmailAttachment>();
        }

        public Guid EmailId { get; set; }

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

        public List<EmailAddress> Recipients { get; set; }

        public string RecipientsUnionAddress
        {
            get
            {

                return Recipients?.UnionAddress();
            }
        }

        public List<EmailAddress> CCs { get; set; }

        public string CCsUnionAddress
        {
            get
            {

                return CCs?.UnionAddress();
            }
        }

        public List<EmailAddress> BCCs { get; set; }

        public string BCCsUnionAddress
        {
            get
            {

                return BCCs?.UnionAddress();
            }
        }

        public List<EmailAddress> ReplyTos { get; set; }

        public string ReplyTosUnionAddress
        {
            get
            {

                return ReplyTos?.UnionAddress();
            }
        }

        public List<EmailAttachment> Attachments { get; set; }

        public List<Tag> Tags { get; set; }

        public bool IsRead { get; set; }
    }
}
