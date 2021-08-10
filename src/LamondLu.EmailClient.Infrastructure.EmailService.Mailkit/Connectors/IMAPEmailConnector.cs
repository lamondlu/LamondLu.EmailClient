using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit.Net.Imap;
using System.Collections.Generic;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnector : IEmailConnector
    {
        private ImapClient _emailClient = null;
        private EmailConnector _emailConnector = null;

        public IMAPEmailConnector(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public void Connect()
        {
            _emailClient = new ImapClient();

            _emailClient.Connect(_emailConnector.Server.Server, _emailConnector.Server.Port, true);
            _emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            _emailClient.Authenticate(_emailConnector.UserName, _emailConnector.Password);

            if (_emailConnector.Server.IsNetEase)
            {
                //for netease box, there need some extra work
                SpeicalBox();
            }
        }

        private void SpeicalBox()
        {
            _emailClient.Identify(new ImapImplementation
            {
                OS = "2.0",
                Name = "xxxxx"
            });
        }

        public void Listen()
        {

        }
    }
}