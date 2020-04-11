using LamondLu.EmailClient.Domain.Enum;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorFactory
    {
         IEmailConnector Build(EmailConnectorType emailConnectorType);
    }
}