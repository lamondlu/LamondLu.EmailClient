using System;

namespace LamondLu.EmailX.Domain
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {

        }

        public EmailTemplate(Guid emailTemplateId, string name, string subject, string body)
        {
            EmailTemplateId = emailTemplateId;
            Name = name;
            Subject = subject;
            Body = body;
        }

        public EmailTemplate(string name, string subject, string body)
        {
            EmailTemplateId = Guid.NewGuid();
            Name = name;
            Subject = subject;
            Body = body;
        }

        public Guid EmailTemplateId { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
