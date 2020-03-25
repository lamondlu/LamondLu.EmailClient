﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DomainModel
{
    public class Tag
    {
        public Guid TagId { get; set; }

        public string TagName { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public bool IsSystem { get; set; }
    }
}