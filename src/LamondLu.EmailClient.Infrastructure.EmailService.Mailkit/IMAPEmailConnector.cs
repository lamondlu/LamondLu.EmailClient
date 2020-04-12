using System.Collections.Generic;
using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnector : IEmailConnector
    {
        public IMAPEmailConnector(List<Rule> rules)
        {
            Pipeline = new RulePipeline(rules);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            throw new System.NotImplementedException();
        }
    }
}