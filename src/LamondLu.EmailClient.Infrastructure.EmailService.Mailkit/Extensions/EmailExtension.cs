using LamondLu.EmailClient.Domain;
using MailKit;
using MimeKit;
using System;
using System.Linq;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.Extensions
{
    public static class EmailExtension
    {
        public static Email ConvertEmail(this MimeMessage message, UniqueId uniqueId, Guid folderId)
        {
            var email = new Email(message.MessageId, uniqueId.Id, uniqueId.Validity);
            email.Subject = message.Subject;

            
            email.Receipts = message.To?.Mailboxes?.Select(p=> new EmailAddress(p.Address, p.Name )).ToList();

            email.CCs = message.Cc?.Mailboxes?.Select(p=> new EmailAddress(p.Address, p.Name )).ToList();

            email.BCCs = message.Bcc?.Mailboxes?.Select(p=> new EmailAddress(p.Address, p.Name )).ToList();


            return email;
        }
    }
}