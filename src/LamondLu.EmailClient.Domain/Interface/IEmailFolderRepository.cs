using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailFolderRepository
    {
        List<EmailFolderConfigurationModel> GetFolders(Guid emailConnectorId);
    }
}
