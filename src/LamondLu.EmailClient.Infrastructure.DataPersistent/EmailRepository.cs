using System;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailRepository : IEmailRepository
    {
        private DapperDbContext _context = null;

        public EmailRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task SaveNewEmail(AddEmailModel email)
        {
            var sql = "INSERT INTO Email(EmailId, EmailConnectorId, Subject, ReceivedDate, EmailFolderId, Id, Validity, CreatedTime, Sender, MessageId) VALUE(@emailId, @emailConnectorId, @subject,@receivedDate, @emailFolderId, @id, @validity, @createdTime, @sender, @messageId)";

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
                email.MessageId
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
            var sql = "INSERT INTO EmailBody(EmailId, EmailBody, EmailHTMLBody) VALUE(@emailId, @emailBody, @emailHTMLBody)";

            await _context.Execute(sql, new { emailId, emailBody, emailHTMLBody});
        }

        
    }
}