using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit.Net.Pop3;
using System.Collections.Generic;


namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class POP3EmailConnector : IEmailConnector
    {
        public POP3EmailConnector(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            using (Pop3Client client = new Pop3Client())
            {

            }
        }

        public void Listen()
        {
            throw new System.NotImplementedException();
        }
    }
}
