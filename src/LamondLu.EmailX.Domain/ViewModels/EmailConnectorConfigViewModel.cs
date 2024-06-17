using LamondLu.EmailX.Domain.Enum;
using System;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class EmailConnectorConfigViewModel
    {
        public Guid EmailConnectorId { get; set; }

        public EmailConnectorType Type { get; set; }

        public EmailConnectorStatus Status { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string POP3Server { get; set; }

        public int? POP3Port { get; set; }

        public string IMAPServer { get; set; }

        public int? IMAPPort { get; set; }

        public string SMTPServer { get; set; }

        public int? SMTPPort { get; set; }

        public bool EnableSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Description{get;set;}

        public bool IsRunning
        {
            get
            {
                return Status == EmailConnectorStatus.Running;
            }
        }

        public bool IsStopped
        {
            get
            {
                return Status == EmailConnectorStatus.Stopped;
            }
        }
    }
}
