using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailAttachmentRepository : IEmailAttachmentRepository
    {
        private DapperDbContext _context = null;

        public EmailAttachmentRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmailAttachmentViewModel>> GetEmailAttachments(Guid emailId)
        {
            var sql = "SELECT EmailAttachmentId, FileName, SourceFileName FROM EmailAttachment WHERE EmailId=@emailId";

            var result = await _context.QueryAsync<EmailAttachmentViewModel>(sql, new {
                emailId
            });

            return result.ToList();
        }
    }
}