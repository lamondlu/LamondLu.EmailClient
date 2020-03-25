using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Enum
{
    public enum RuleType
    {
        Forward = 0,
        Reply = 1,
        Classify = 2,
        Custom = 99
    }
}
