using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailFolderRepository
    {
        Task<EmailFolderConfigurationModel> GetEmailFolder(Guid emailConnectorId, string folderPath);

        Task<EmailFolderConfigurationModel> CreateEmailFolder(Guid emailConnectorId, string folderPath);


        Task<List<EmailFolderConfigurationModel>> GetFolders(Guid emailConnectorId);
    }
}
