using LamondLu.EmailX.Domain.Enum;
using System;
using System.Collections.Generic;

namespace LamondLu.EmailX.Domain
{
    public class EmailConnector
    {
        public EmailConnector(string name, string emailAddress, string userName, string password, EmailServerConfig config, EmailConnectorType type, string description): this(Guid.NewGuid(), name, emailAddress, userName, password, config, type, description)
        {
            

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
            IMAPPort = config.IMAPPort;
            IMAPServer = config.IMAPServer;
            POP3Port = config.POP3Port;
            POP3Server = config.POP3Server;
            SMTPPort = config.SMTPPort;
            SMTPServer = config.SMTPServer;
            EnableSSL = config.EnableSSL;
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

        public string SMTPServer { get; set; }

        public int? SMTPPort { get; set; }

        public string POP3Server { get; set; }

        public int? POP3Port { get; set; }

        public string IMAPServer { get; set; }

        public int? IMAPPort { get; set; }

        public string Description { get; set; }

        public bool EnableSSL { get; set; }

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
