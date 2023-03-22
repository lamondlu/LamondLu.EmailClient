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

            await _context.Execute(sql, new {
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
    }
}