using LamondLu.EmailClient.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Domain
{
    public class EmailConnector
    {
        public EmailConnector(string name, string emailAddress, string userName, string password, EmailServerConfig config, EmailConnectorType type, string description)
        {
            EmailConnectorId = Guid.NewGuid();
            Name = name;
            UserName = userName;
            Password = password;
            Server = config;
            Type = type;
            Description = description;
            Status = EmailConnectorStatus.Stopped;
            EmailAddress = emailAddress;
        }

        public EmailConnector(Guid emailConnectorId, string name, string emailAddress, string userName, string password, EmailServerConfig config, EmailConnectorType type, string description)
        {
            EmailConnectorId = emailConnectorId;
            Name = name;
            UserName = userName;
            Password = password;
            Server = config;
            Type = type;
            Description = description;
            Status = EmailConnectorStatus.Stopped;
            EmailAddress = emailAddress;
        }

        public Guid EmailConnectorId { get; private set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Rule> Rules { get; set; }

        public EmailConnectorStatus Status { get; private set; }

        public EmailServerConfig Server { get; set; }

        public EmailConnectorType Type { get; set; }

        public string Description { get; set; }

        public void Start()
        {
            this.Status = EmailConnectorStatus.Running;
        }

        public void Stop()
        {
            this.Status = EmailConnectorStatus.Stopped;
        }
    }
}
