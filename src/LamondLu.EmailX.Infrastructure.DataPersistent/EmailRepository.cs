using System;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Enum;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailRepository : IEmailRepository
    {
        private DapperDbContext _context = null;

        public EmailRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task SaveEmail(Email email)
        {
            //save email into database
            var sql =
                "INSERT INTO Email(EmailId, EmailConnectorId, Subject, ReceivedDate, EmailFolderId, Id, Validity, CreatedTime, Sender, MessageId, IsRead, `To`, Cc, Bcc, ReplyTo) VALUE(@EmailId, @EmailConnectorId, @Subject,@ReceivedDate, @EmailFolderId, @Id, @Validity, @CreatedTime, @Sender, @MessageId, @IsRead, @To, @Cc, @Bcc, @ReplyTo)";

            await _context.Execute(sql, new
            {
                EmailId = email.EmailId,
                email.EmailFolder.EmailConnectorId,
                email.Subject,
                email.ReceivedDate,
                email.EmailFolder.EmailFolderId,
                Id = email.EmailRealId,
                Validity = email.EmailValidityId,
                CreatedTime = DateTime.Now,
                Sender = email.Sender.Address,
                email.MessageId,
                email.IsRead,
                To = email.RecipientsUnionAddress,
                Cc = email.CCsUnionAddress,
                Bcc = email.BCCsUnionAddress,
                ReplyTo = email.ReplyTosUnionAddress
            });

            if (email.Attachments != null && email.Attachments.Any())
            {
                foreach (var attachment in email.Attachments)
                {
                    var attachmentSQL = "INSERT INTO EmailAttachment(EmailAttachmentId, EmailId, FileName, FileSize, SourceFileName) VALUE(@emailAttachmentId, @emailId, @fileName, @fileSize, @sourceFileName)";

                    await _context.Execute(attachmentSQL, new
                    {
                        attachment.EmailAttachmentId,
                        email.EmailId,
                        attachment.FileName,
                        attachment.FileSize,
                        sourceFileName = attachment.SystemFileName
                    });
                }
            }

            var bodySQL =
                "INSERT INTO EmailBody(EmailId, EmailBody, EmailHTMLBody) VALUE(@EmailId, @EmailBody, @EmailHTMLBody)";

            await _context.Execute(bodySQL, new
            {
                EmailId = email.EmailId,
                EmailBody = email.TextBody,
                EmailHTMLBody = email.Body
            });

            if (email.Recipients != null && email.Recipients.Any())
            {
                foreach (var recipient in email.Recipients)
                {
                    var recipientSQL =
                        "INSERT INTO EmailRecipient(EmailRecipientId, EmailId, Email, DisplayName, Type) VALUE(@EmailRecipientId, @EmailId, @Email, @DisplayName, @Type)";

                    await _context.Execute(recipientSQL, new
                    {
                        EmailRecipientId = Guid.NewGuid(),
                        EmailId = email.EmailId,
                        Email = recipient.Address,
                        recipient.DisplayName,
                        Type = EmailRecipientType.To
                    });
                }
            }

            if(email.CCs != null && email.CCs.Any())
            {
                foreach (var cc in email.CCs)
                {
                    var ccSQL =
                        "INSERT INTO EmailRecipient(EmailRecipientId, EmailId, Email, DisplayName, Type) VALUE(@EmailRecipientId, @EmailId, @Email, @DisplayName, @Type)";

                    await _context.Execute(ccSQL, new
                    {
                        EmailRecipientId = Guid.NewGuid(),
                        EmailId = email.EmailId,
                        Email = cc.Address,
                        cc.DisplayName,
                        Type = EmailRecipientType.Cc
                    });
                }
            }

            if(email.BCCs != null && email.BCCs.Any())
            {
                foreach (var bcc in email.BCCs)
                {
                    var bccSQL =
                        "INSERT INTO EmailRecipient(EmailRecipientId, EmailId, Email, DisplayName, Type) VALUE(@EmailRecipientId, @EmailId, @Email, @DisplayName, @Type)";

                    await _context.Execute(bccSQL, new
                    {
                        EmailRecipientId = Guid.NewGuid(),
                        EmailId = email.EmailId,
                        Email = bcc.Address,
                        bcc.DisplayName,
                        Type = EmailRecipientType.Bcc
                    });
                }
            }

            if(email.ReplyTos != null && email.ReplyTos.Any())
            {
                foreach (var replyTo in email.ReplyTos)
                {
                    var replyToSQL =
                        "INSERT INTO EmailRecipient(EmailRecipientId, EmailId, Email, DisplayName, Type) VALUE(@EmailRecipientId, @EmailId, @Email, @DisplayName, @Type)";

                    await _context.Execute(replyToSQL, new
                    {
                        EmailRecipientId = Guid.NewGuid(),
                        EmailId = email.EmailId,
                        Email = replyTo.Address,
                        replyTo.DisplayName,
                        Type = EmailRecipientType.ReplyTo
                    });
                }
            }
        }



        public async Task SaveNewEmail(AddEmailModel email)
        {
            var sql =
                "INSERT INTO Email(EmailId, EmailConnectorId, Subject, ReceivedDate, EmailFolderId, Id, Validity, CreatedTime, Sender, MessageId, IsRead, `To`, Cc, Bcc, ReplyTo) VALUE(@emailId, @emailConnectorId, @subject,@receivedDate, @emailFolderId, @id, @validity, @createdTime, @sender, @messageId, @isRead, @to, @cc, @bcc, @replyTo)";

            await _context.Execute(sql, new
            {
                email.EmailId,
                email.EmailConnectorId,
                email.Subject,
                email.ReceivedDate,
                email.Id,
                email.Validity,
                email.CreatedTime,
                email.EmailFolderId,
                email.Sender,
                email.MessageId,
                email.IsRead,
                email.To,
                email.Cc,
                email.Bcc,
                email.ReplyTo
            });
        }

        public async Task<bool> MessageIdExisted(string messageId)
        {
            var sql = "SELECT messageId FROM Email WHERE messageId=@messageId limit 1";

            var result = await _context.QueryFirstOrDefaultAsync<string>(sql, new
            {
                messageId
            });

            return !string.IsNullOrWhiteSpace(result);
        }

        public async Task SaveEmailBody(Guid emailId, string emailBody, string emailHTMLBody)
        {
            var sql =
                "INSERT INTO EmailBody(EmailId, EmailBody, EmailHTMLBody) VALUE(@emailId, @emailBody, @emailHTMLBody)";

            await _context.Execute(sql, new { emailId, emailBody, emailHTMLBody });
        }

        public async Task<PagedResult<EmailListViewModel>> GetEmails(Guid emailConnectorId, int pageSize, int pageNum)
        {
            var sql =
                $"SELECT e.IsRead, e.EmailId, e.Subject, e.Sender, ec.EmailAddress as 'To', e.ReceivedDate, e.Id, e.Validity, e.MessageId FROM Email e INNER JOIN EmailConnector ec ON e.EmailConnectorId=e.EmailConnectorId WHERE e.EmailConnectorId=@emailConnectorId ORDER BY ReceivedDate DESC LIMIT @skipNum, @pageSize";


            var result = await _context.QueryAsync<EmailListViewModel>(sql,
                new { emailConnectorId, skipNum = (pageNum - 1) * pageSize, pageSize });

            var countSQL =
                "SELECT COUNT(*) FROM Email e INNER JOIN EmailConnector ec ON e.EmailConnectorId=e.EmailConnectorId WHERE e.EmailConnectorId=@emailConnectorId";

            var total = await _context.QueryFirstOrDefaultAsync<int>(countSQL, new { emailConnectorId });

            return new PagedResult<EmailListViewModel>(result.ToList(), total, pageSize, pageNum);
        }

        public async Task<EmailDetailedViewModel> GetEmail(Guid emailId)
        {
            var sql =
                "SELECT e.EmailId, e.Subject, e.Sender, ec.EmailAddress as 'To', e.ReceivedDate, eb.EmailHTMLBody FROM Email e INNER JOIN EmailConnector ec ON e.EmailConnectorId=e.EmailConnectorId INNER JOIN EmailBody eb ON e.EmailId=eb.EmailId WHERE e.EmailId=@emailId";

            var email = await _context.QueryFirstOrDefaultAsync<EmailDetailedViewModel>(sql, new { emailId });

            return email;
        }

        public async Task<string> GetEmailBody(Guid emailId)
        {
            var sql = "SELECT EmailHTMLBody FROM EmailBody WHERE EmailId=@emailId";

            return await _context.QueryFirstOrDefaultAsync<string>(sql, new { emailId });
        }
    }
}