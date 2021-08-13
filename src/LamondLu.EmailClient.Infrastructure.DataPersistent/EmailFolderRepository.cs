using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
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
            var sql = "SELECT EmailFolderId FROM EmailFolder WHERE IsDeleted=0";

            return null;
        }

        public async Task<EmailFolderConfigurationModel> CreateEmailFolder(Guid emailConnectorId, string folderPath, string folderName)
        {
            var sql = "INSERT INTO EmailFolder(FolderId, EmailConnectorId, FolderFullPath,FolderName,IsDeleted,LastEmailId,LastValidityId) VALUES(UUID(),@emailConnectorId,@folderPath,@folderName,0,0,0)";

            await _context.Execute(sql, new
            {
                emailConnectorId,
                folderName,
                folderPath
            });

            await _context.SubmitAsync();

            return await GetEmailFolder(emailConnectorId, folderPath);
        }

        public async Task<EmailFolderConfigurationModel> GetEmailFolder(Guid emailConnectorId, string folderPath)
        {
            var sql = "SELECT * FROM EmailFolder WHERE EmailConnectorId=@emailConnectorId AND FolderFullPath=@folderPath";

            return await _context.QueryFirstOrDefaultAsync<EmailFolderConfigurationModel>(sql, new
            {
                emailConnectorId,
                folderPath
            });
        }
    }
}
