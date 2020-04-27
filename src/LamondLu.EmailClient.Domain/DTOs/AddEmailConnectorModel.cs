using LamondLu.EmailClient.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LamondLu.EmailClient.Domain.DTOs
{
    public class AddEmailConnectorModel
    {
        [DisplayName("Connector Name")]
        public string Name { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        [DisplayName("SSL/TLS")]
        public bool EnableSSL { get; set; }

        public EmailConnectorType Type { get; set; }

        public string Description { get; set; }
    }
}
