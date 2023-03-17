using LamondLu.EmailClient.Domain;
using MailKit;
using MimeKit;
using System;
using System.IO;
using System.Linq;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.Extensions
{
    public static class EmailExtension
    {
        public static Email ConvertEmail(this MimeMessage message, UniqueId uniqueId, Guid folderId)
        {
            var email = new Email(message.MessageId, uniqueId.Id, uniqueId.Validity);
            email.Subject = message.Subject;
            email.EmailFolder = new EmailFolder(folderId);
            email.Receipts = message.To?.Mailboxes?.Select(p => new EmailAddress(p.Address, p.Name)).ToList();
            email.CCs = message.Cc?.Mailboxes?.Select(p => new EmailAddress(p.Address, p.Name)).ToList();
            email.BCCs = message.Bcc?.Mailboxes?.Select(p => new EmailAddress(p.Address, p.Name)).ToList();
            email.Body = PopulateInlineImages(message);
            email.ReceivedDate = message.Date.LocalDateTime;
            email.EmailValidityId = uniqueId.Validity;
            email.EmailRealId = uniqueId.Id;
            email.MessageId = message.MessageId;

            return email;
        }


        private static string PopulateInlineImages(MimeMessage newMessage)
        {
            var body = string.Empty;

            if (!string.IsNullOrEmpty(newMessage.HtmlBody))
            {
                body = newMessage.HtmlBody;

                foreach (var entity in newMessage.BodyParts)
                {
                    if (entity is MimePart)
                    {
                        var att = entity as MimePart;
                        if (att.ContentId != null && att.Content != null && att.ContentType.MediaType == "image" && (body.IndexOf("cid:" + att.ContentId) > -1))
                        {
                            byte[] b;
                            using (var mem = new MemoryStream())
                            {
                                att.Content.DecodeTo(mem);
                                b = mem.ToArray();
                            }

                            string imageBase64 = "data:" + att.ContentType.MimeType + ";base64," + System.Convert.ToBase64String(b);
                            body = body.Replace("cid:" + att.ContentId, imageBase64);
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(newMessage.TextBody))
            {
                body = newMessage.TextBody.Replace("\r\n", "<br /><br />");
            }
            else
            {
                body = "<div>&nbsp;</div>";
            }

            return body;
        }
    }
}