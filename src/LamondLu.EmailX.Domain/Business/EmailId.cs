using System;

namespace LamondLu.EmailX.Domain.Business
{
    public class EmailId
    {
        public EmailId(string messageId, uint mailkitId, uint mailkitValidityId)
        {
            SystemId = Guid.NewGuid();
            this.MessageId = messageId;
            this.MailkitId = mailkitId;
            this.MailkitValidityId = mailkitValidityId;
        }

        public Guid SystemId { get; private set; }

        public string MessageId { get; private set; }

        public uint MailkitId { get; private set; }

        public uint MailkitValidityId { get; private set; }
    }
}
