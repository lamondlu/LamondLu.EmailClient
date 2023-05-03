using System;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Domain.ViewModels;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
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
            var sql =
                "INSERT INTO Email(EmailId, EmailConnectorId, Subject, ReceivedDate, EmailFolderId, Id, Validity, CreatedTime, Sender, MessageId) VALUE(@emailId, @emailConnectorId, @subject,@receivedDate, @emailFolderId, @id, @validity, @createdTime, @sender, @messageId)";

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
            var sql =
                "INSERT INTO EmailBody(EmailId, EmailBody, EmailHTMLBody) VALUE(@emailId, @emailBody, @emailHTMLBody)";

            await _context.Execute(sql, new { emailId, emailBody, emailHTMLBody });
        }

        public async Task<PagedResult<EmailListViewModel>> GetEmails(Guid emailConnectorId, int pageSize, int pageNum)
        {
            var sql =
                $"SELECT e.EmailId, e.Subject, e.Sender, ec.EmailAddress as 'To', e.ReceivedDate, e.Id, e.Validity, e.MessageId FROM Email e INNER JOIN EmailConnector ec ON e.EmailConnectorId=e.EmailConnectorId WHERE e.EmailConnectorId=@emailConnectorId ORDER BY ReceivedDate DESC LIMIT @skipNum, @pageSize"; 
            

            var result = await _context.QueryAsync<EmailListViewModel>(sql,
                new { emailConnectorId, skipNum = (pageNum - 1) * pageSize, pageSize });

            var countSQL =
                "SELECT COUNT(*) FROM Email e INNER JOIN EmailConnector ec ON e.EmailConnectorId=e.EmailConnectorId WHERE e.EmailConnectorId=@emailConnectorId";

            var total = await _context.QueryFirstOrDefaultAsync<int>(countSQL, new { emailConnectorId });

            return new PagedResult<EmailListViewModel>(result.ToList(), total, pageSize, pageNum);
        }
    }
}