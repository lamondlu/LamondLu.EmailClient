using LamondLu.EmailClient.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.DTOs
{
    public class AddEmailConnectorModel
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EmailConnectorStatus Status { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public EmailConnectorType Type { get; set; }

        public string Description { get; set; }
    }
}
