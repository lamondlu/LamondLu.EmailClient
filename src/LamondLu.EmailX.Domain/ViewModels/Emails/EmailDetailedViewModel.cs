using System;

namespace LamondLu.EmailX.Domain.ViewModels.Emails
{
    public class EmailDetailedViewModel
    {
        public Guid EmailId { get; set; }

        public string Subject { get; set; }

        public string Sender { get; set; }

        public string To { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string EmailHTMLBody { get; set; }
    }
}