using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnector : IEmailConnector
    {
        public IMAPEmailConnector(List<Rule> rules, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(rules, ruleProcessorFactory, unitOfWork);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            throw new System.NotImplementedException();
        }
    }
}