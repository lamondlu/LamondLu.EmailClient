using System;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class EmailFolderConfigurationModel
    {
        public Guid EmailFolderId { get; set; }

        public Guid EmailConnectorId { get; set; }

        public string FolderPath { get; set; }

        public uint LastEmailId { get; set; }

        public uint LastValidityId { get; set; }
    }
}
