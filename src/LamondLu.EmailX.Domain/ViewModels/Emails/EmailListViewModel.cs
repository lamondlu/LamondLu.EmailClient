using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.ViewModels.Emails
{
    public class EmailListViewModel
    {
        public Guid EmailId { get; set; }

        public string Subject { get; set; }

        public string Sender { get; set; }

        public string To { get; set; }

        public DateTime ReceivedDate { get; set; }

        public long Id { get; set; }

        public long Validity { get; set; }

        public string MessageId { get; set; }

        public bool IsRead { get; set; }
    }
}