using LamondLu.EmailClient.Domain.Enum;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain
{
    public class EmailFolder
    {
        public string FolderName { get; set; }

        public FolderType FolderType { get; set; }

        public List<EmailFolder> SubFolders { get; set; }
    }
}
