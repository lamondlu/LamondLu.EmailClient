﻿using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailFolderRepository
    {
        Task<EmailFolder> GetEmailFolder(Guid emailConnectorId, string folderPath);

        Task<EmailFolder> CreateEmailFolder(Guid emailConnectorId, string folderPath, string folderName);

        Task<List<EmailFolderConfigurationModel>> GetFolders(Guid emailConnectorId);

        Task RecordFolderProcess(Guid folderId, uint lastEmailId, uint lastValidityId);
    }
}
