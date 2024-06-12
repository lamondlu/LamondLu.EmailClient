using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class ForwardRule : Rule
    {
        public List<EmailAddress> ForwardAddresses { get; set; }
    }
}
