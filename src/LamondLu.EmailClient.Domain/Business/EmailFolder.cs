using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain
{
    public class EmailFolder
    {
        public string FolderName { get; set; }

        public List<EmailFolder> SubFolders { get; set; }
    }
}
