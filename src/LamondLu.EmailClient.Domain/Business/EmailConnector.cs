using LamondLu.EmailClient.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain
{
    public class EmailConnector
    {
        public Guid EmailConnectorId { get; private set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EmailConnectorStatus Status { get; set; }

        public EmailServerConfig Server { get; set; }

        public EmailConnectorType Type { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
