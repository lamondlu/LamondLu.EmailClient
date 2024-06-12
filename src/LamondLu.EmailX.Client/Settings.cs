using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Client
{
    public class Settings
    {
        public EmailConnectorType Type { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}