using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit.Net.Pop3;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class POP3EmailConnectorWorker : IEmailConnectorWorker
    {
        public POP3EmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public async Task<bool> Connect()
        {
            throw new System.NotImplementedException();
        }

        public async Task Listen()
        {
            throw new System.NotImplementedException();
        }
    }
}
