using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailRecipientRepository : IEmailRecipientRepository
    {
        private DapperDbContext _context = null;

        public EmailRecipientRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task SaveEmailReceipt(AddEmailRecipientModel emailRecipient)
        {
            var sql = "INSERT INTO EmailRecipient(EmailRecipientId, EmailId, Email, DisplayName, `Type`) VALUES (@EmailRecipientId, @EmailId, @MailboxAddress, @DisplayName, @type)";

            await _context.Execute(sql, new
            {
                emailRecipient.EmailRecipientId,
                emailRecipient.EmailId,
                emailRecipient.MailboxAddress,
                emailRecipient.DisplayName,
                emailRecipient.Type
            });
        }

        public async Task<List<EmailRecipientViewModel>> GetEmailRecipients(Guid emailId)
        {
            var sql = "SELECT Email, DisplayName, `Type` FROM EmailRecipient WHERE EmailId=@emailId";

            var result =  await _context.QueryAsync<EmailRecipientViewModel>(sql, new
            {
                emailId
            });
            
            return result.ToList();
        }
    }
}