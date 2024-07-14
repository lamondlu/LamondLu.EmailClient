using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LamondLu.EmailX.Domain;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class EmailFolderRepository : IEmailFolderRepository
    {
        private DapperDbContext _context = null;

        public EmailFolderRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmailFolderConfigurationModel>> GetFolders(Guid emailConnectorId)
        {
            var sql = "SELECT * FROM EmailFolder WHERE IsDeleted=0 and EmailConnectId=@emailConnectorId";

            var result = await _context.QueryAsync<EmailFolderConfigurationModel>(sql, new
            {
                emailConnectorId
            });

            return result.ToList();
        }

        public async Task<EmailFolder> CreateEmailFolder(Guid emailConnectorId, string folderPath, string folderName)
        {
            var sql = "INSERT INTO EmailFolder(EmailFolderId, EmailConnectorId, FolderFullPath,FolderName,IsDeleted,LastEmailId,LastValidityId) VALUES(UUID(),@emailConnectorId,@folderPath,@folderName,0,1,0)";

            await _context.Execute(sql, new
            {
                emailConnectorId,
                folderName,
                folderPath
            });

            await _context.SubmitAsync();

            return await GetEmailFolder(emailConnectorId, folderPath);
        }

        public async Task<EmailFolder> GetEmailFolder(Guid emailConnectorId, string folderPath)
        {
            var sql = "SELECT * FROM EmailFolder WHERE EmailConnectorId=@emailConnectorId AND FolderFullPath=@folderPath";

            return await _context.QueryFirstOrDefaultAsync<EmailFolder>(sql, new
            {
                emailConnectorId,
                folderPath
            });
        }

        public async Task RecordFolderProcess(Guid folderId, uint lastEmailId, uint lastValidityId)
        {
            var sql = "UPDATE EmailFolder SET LastEmailId=@lastEmailId, LastValidityId=@lastValidityId WHERE EmailFolderId=@folderId";

            await _context.Execute(sql, new
            {
                folderId,
                lastEmailId,
                lastValidityId
            });
        }
    }
}
