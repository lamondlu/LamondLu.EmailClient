using System;
using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.DTOs
{
    public class AddEmailRecipientModel
    {
        public Guid EmailRecipientId { get; private set; } = Guid.NewGuid();

        public Guid EmailId { get; set; }

        public string MailboxAddress { get; set; }

        public string DisplayName { get; set; }

        public EmailRecipientType Type { get; set; }
    }
}