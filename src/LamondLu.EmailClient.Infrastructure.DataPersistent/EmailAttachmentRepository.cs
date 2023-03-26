using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailAttachmentRepository : IEmailAttachmentRepository
    {
        private DapperDbContext _context = null;

        public EmailAttachmentRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task AddEmailAttachment(AddEmailAttachmentModel dto)
        {
            var sql = "INSERT INTO EmailAttachment(EmailAttachmentId, EmailId, FileName, FileSize, SourceFileName) VALUE(@emailAttachmentId, @emailId, @fileName, @fileSize, @sourceFileName)";

            await _context.Execute(sql, new {
                dto.EmailAttachmentId,
                dto.EmailId,
                dto.FileName,
                dto.FileSize,
                dto.SourceFileName
            });
        }
    }
}