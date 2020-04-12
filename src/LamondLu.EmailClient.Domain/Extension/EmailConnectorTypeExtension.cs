using LamondLu.EmailClient.Domain.Enum;

namespace LamondLu.EmailClient.Domain.Extension
{
    public static class EmailConnectorTypeExtension
    {
        public static bool IsPop3(this EmailConnectorType type)
        {
            return type == EmailConnectorType.POP3;
        }

        public static bool IsIMAP(this EmailConnectorType type)
        {
            return type == EmailConnectorType.IMAP;
        }
    }
}