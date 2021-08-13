using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailFolderRepository : IEmailFolderRepository
    {
        public List<EmailFolderConfigurationModel> GetFolders(Guid emailConnectorId)
        {
            var sql = "SELECT EmailFolderId FROM EmailFolder WHERE IsDeleted=0";

            return null;
        }
    }
}
