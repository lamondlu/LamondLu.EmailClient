using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class EmailConnectorFactory : IEmailConnectorFactory
    {
        public IEmailConnector Build(EmailConnectorType emailConnectorType)
        {
            throw new System.NotImplementedException();
        }
    }
}