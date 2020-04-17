using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain
{
    public class EmailServerConfig
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }
    }
}
