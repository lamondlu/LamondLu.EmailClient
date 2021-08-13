using LamondLu.EmailClient.Domain.Enum;
using System;

namespace LamondLu.EmailClient.Domain.ViewModels
{
    public class EmailConnectorConfigViewModel
    {
        public Guid EmailConnectorId { get; set; }

        public EmailConnectorType Type { get; set; }

        public EmailConnectorStatus Status { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

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
