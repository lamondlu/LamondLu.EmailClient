using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.DTOs
{
    public class AddEmailAttachmentModel
    {
        public Guid EmailAttachmentId { get; set; }

        public Guid EmailId { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string SourceFileName { get; set; }
    }
}