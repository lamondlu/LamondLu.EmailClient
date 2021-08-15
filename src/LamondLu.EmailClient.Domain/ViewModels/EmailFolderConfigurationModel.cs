﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.ViewModels
{
    public class EmailFolderConfigurationModel
    {
        public Guid FolderId { get; set; }

        public Guid EmailConnectorId { get; set; }

        public string FolderPath { get; set; }

        public uint LastEmailId { get; set; }

        public uint LastValidityId { get; set; }
    }
}
