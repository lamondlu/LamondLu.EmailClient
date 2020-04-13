using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.ViewModels
{
    public class EmailConnectorConfigViewModel
    {
        public Guid EmailConnectorId { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
