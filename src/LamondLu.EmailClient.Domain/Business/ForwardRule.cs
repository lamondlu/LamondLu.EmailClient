using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain
{
    public class ForwardRule : Rule
    {
        public List<EmailAddress> ForwardAddresses { get; set; }
    }
}
