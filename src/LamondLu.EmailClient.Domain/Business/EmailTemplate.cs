using System;

namespace LamondLu.EmailClient.Domain
{
    public class EmailTemplate
    {
        public Guid EmailTemplateId { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
