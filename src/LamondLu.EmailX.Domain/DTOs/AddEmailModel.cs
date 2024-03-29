using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.DTOs
{
    public class AddEmailModel
    {
        public Guid EmailId { get; private set; } = Guid.NewGuid();

        public Guid EmailConnectorId { get; set; }

        public string Subject { get; set; }

        public DateTime ReceivedDate { get; set; }

        public Guid EmailFolderId { get; set; }

        public uint Id { get; set; }

        public uint Validity { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string Sender { get; set; }

        public string MessageId { get; set; }

        public bool IsRead { get; set; }
    }
}