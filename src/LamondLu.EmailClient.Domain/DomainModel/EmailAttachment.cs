﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DomainModel
{
    public class EmailAttachment
    {
        public string FileName { get; set; }

        public long FileSize { get; set; }
    }
}
