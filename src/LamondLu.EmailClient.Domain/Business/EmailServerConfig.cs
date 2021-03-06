﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain
{
    public class EmailServerConfig
    {
        public EmailServerConfig(string server, int port, bool enableSSL)
        {
            Server = server;
            Port = port;
            EnableSSL = enableSSL;
        }

        public string Server { get; private set; }

        public int Port { get; private set; }

        public bool EnableSSL { get; private set; }
    }
}
