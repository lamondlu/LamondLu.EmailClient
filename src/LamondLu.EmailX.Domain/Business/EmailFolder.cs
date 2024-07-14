using LamondLu.EmailX.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class EmailFolder
    {
        public Guid EmailFolderId { get; set; }

        public Guid EmailConnectorId { get; set; }

        public FolderType FolderType
        {
            get
            {
                if (!string.IsNullOrEmpty(FolderPath) && FolderPath.Contains("Sent"))
                {
                    return FolderType.Send;
                }
                else
                {
                    return FolderType.Receive;
                }
            }
        }

        public List<EmailFolder> SubFolders { get; set; }

        public string FolderPath { get; set; }

        public uint LastEmailId { get; set; }

        public uint LastValidityId { get; set; }
    }
}
