using LamondLu.EmailX.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class EmailFolder
    {
        public EmailFolder(Guid folderId)
        {
            FolderId = folderId;
        }

        public Guid FolderId { get; set; }

        public string FolderName { get; set; }

        public FolderType FolderType { get; set; }

        public List<EmailFolder> SubFolders { get; set; }
    }
}
