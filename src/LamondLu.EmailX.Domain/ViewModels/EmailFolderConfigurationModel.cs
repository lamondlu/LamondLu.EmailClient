using System;
using System.Collections.Generic;
using System.Text;

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
