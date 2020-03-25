﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DomainModel
{
    public class Email
    {
        public Email()
        {
            EmailId = Guid.NewGuid();
            Tags = new List<Tag>();
            Attachments = new List<EmailAttachment>();
        }

        public Guid EmailId { get; private set; }

        public DateTime ReceivedDate { get; set; }

        public string Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Notes { get; set; }

        public string Receipt { get; set; }

        public List<EmailAttachment> Attachments { get; set; }

        public List<Tag> Tags { get; set; }
    }
}