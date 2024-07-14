using System;

namespace LamondLu.EmailX.Domain
{
    public class EmailAttachment
    {
        public Guid EmailAttachmentId { get; set; } = Guid.NewGuid();

        public string FileName { get; set; }

        public string SystemFileName { get; set; }

        public long FileSize { get; set; }
    }
}
