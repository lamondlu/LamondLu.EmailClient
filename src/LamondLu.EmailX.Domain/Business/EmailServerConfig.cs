namespace LamondLu.EmailX.Domain
{
    public class EmailServerConfig
    {
        public EmailServerConfig(string smtpServer, int? smtpPort, string imapServer, int? imapPort, string pop3Server, int? pop3Port ,bool enableSSL)
        {
            SMTPServer = smtpServer;
            SMTPPort = smtpPort;
            IMAPServer = imapServer;
            IMAPPort = imapPort;
            POP3Server = pop3Server;
            POP3Port = pop3Port;
            EnableSSL = enableSSL;
        }

        public string SMTPServer { get; set; }

        public int? SMTPPort { get; set; }

        public string POP3Server { get; set; }

        public int? POP3Port { get; set; }

        public string IMAPServer { get; set; }

        public int? IMAPPort { get; set; }

        public bool EnableSSL { get; private set; }
    }
}
