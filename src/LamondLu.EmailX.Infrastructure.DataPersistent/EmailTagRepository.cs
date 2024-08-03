using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels.Emails;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailTagRepository : IEmailTagRepository
    {
        private DapperDbContext _context = null;

        public EmailTagRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task AddTagToEmail(Guid emailId, Tag tag)
        {
            var sql = "INSERT INTO emailtags(EmailId, TagId) VALUES(@emailId, @tagId)";

            await _context.Execute(sql, new
            {
                emailId,
                tagId = tag.TagId
            });
        }

        public async Task RemoveTagFromEmail(Guid emailId, Tag tag)
        {
            var sql = "UPDATE emailtags SET IsDeleted=1 WHERE EmailId=@emailId AND TagId=@tagId";

            await _context.Execute(sql, new
            {
                emailId,
                tagId = tag.TagId
            });
        }
    }
}