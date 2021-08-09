using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.ViewModels;
using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorTask
    {
        private EmailConnectorConfigViewModel _emailConnector = null;

        public EmailConnectorTask(EmailConnectorConfigViewModel emailConnector)
        {
            _emailConnector = emailConnector;
        }

        public void Start()
        {
            Console.WriteLine($"Email Connector (id:{_emailConnector.EmailConnectorId},name: {_emailConnector.Name}) Start");

            EmailConnector emailConnector = new EmailConnector(_emailConnector.Name, _emailConnector.EmailAddress, _emailConnector.UserName, _emailConnector.Password, new EmailServerConfig(_emailConnector.IP, _emailConnector.Port, _emailConnector.EnableSSL)
            , _emailConnector.Type, string.Empty);

            try
            {
                if (_emailConnector.Type == Domain.Enum.EmailConnectorType.IMAP)
                {
                    StartIMAP(emailConnector);
                }
                else
                {
                    StartPOP3(emailConnector);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void StartIMAP(EmailConnector emailConnector)
        {
            ImapClient emailClient = new ImapClient();

            Console.WriteLine($"Try to connect Email IMAP Server,connector name is {emailConnector.Name}");
            emailClient.Connect(emailConnector.Server.Server, emailConnector.Server.Port, true);
            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            emailClient.Authenticate(emailConnector.UserName, emailConnector.Password);

            if (emailClient.Inbox != null)
            {
                emailClient.Inbox.Open(MailKit.FolderAccess.ReadOnly);
            }

            List<MailKit.UniqueId> ids = null;

            var folders = emailClient.GetFolders(emailClient.PersonalNamespaces[0]);

            foreach (var folder in folders)
            {
                Console.WriteLine($"Current Folder: {folder.Name}");

                if (!folder.IsOpen)
                {
                    folder.Open(FolderAccess.ReadOnly);
                }

                if (folder != null)
                {
                    var range = new UniqueIdRange(new UniqueId((uint)1), UniqueId.MaxValue);
                    ids = folder.Search(MailKit.Search.SearchQuery.Uids(range)).ToList();
                }

                if (ids.Count != 0)
                {
                    foreach (var emailId in ids)
                    {
                        var email = folder.GetMessage(emailId);

                        Console.WriteLine($"[{email.Date}] {email.Subject}");
                    }
                }
            }

        }

        private void StartPOP3(EmailConnector emailConnector)
        {

        }
    }
}
