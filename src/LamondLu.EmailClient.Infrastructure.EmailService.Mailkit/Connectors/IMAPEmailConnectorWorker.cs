using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnectorWorker : IEmailConnectorWorker
    {
        private ImapClient _emailClient = null;
        private EmailConnector _emailConnector = null;

        public IMAPEmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public async Task<bool> Connect()
        {
            try
            {
                _emailClient = new ImapClient();

                await _emailClient.ConnectAsync(_emailConnector.Server.Server, _emailConnector.Server.Port, true);
                _emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await _emailClient.AuthenticateAsync(_emailConnector.UserName, _emailConnector.Password);

                if (_emailConnector.Server.IsNetEase)
                {
                    //for netease box, there need some extra work
                    await SpeicalBox();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task SpeicalBox()
        {
            await _emailClient.IdentifyAsync(new ImapImplementation
            {
                OS = "2.0",
                Name = "xxxxx"
            });
        }

        public async Task Listen()
        {
            Console.WriteLine("Start to pull email.");

            if (_emailClient.Inbox != null)
            {
                _emailClient.Inbox.Open(MailKit.FolderAccess.ReadOnly);
            }

            List<MailKit.UniqueId> ids = null;
            if (_emailClient.Inbox != null)
            {
                var range = new UniqueIdRange(new UniqueId((uint)1), UniqueId.MaxValue);
                ids = _emailClient.Inbox.Search(MailKit.Search.SearchQuery.Uids(range)).Where(x => x.Id > (uint)1).OrderBy(x => x.Id).Take(100).ToList();
            }

            if (ids.Count != 0)
            {
                foreach (var emailId in ids)
                {
                    var email = _emailClient.Inbox.GetMessage(emailId);

                    Console.WriteLine($"[{email.Date}] {email.Subject}");
                }
            }
        }
    }
}