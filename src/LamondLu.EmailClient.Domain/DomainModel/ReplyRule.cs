﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DomainModel
{
    public class ReplyRule : Rule
    {
        public EmailTemplate EmailTemplate { get; set; }
    }
}
