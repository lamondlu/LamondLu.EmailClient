using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit.Net.Imap;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnector : IEmailConnector
    {
        private ImapClient _imapClient = null;

        public IMAPEmailConnector(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            _imapClient = new ImapClient();
        }

        public void Listen()
        {
            throw new System.NotImplementedException();
        }
    }
}