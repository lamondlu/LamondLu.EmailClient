using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnectorWorker : IEmailConnectorWorker
    {
        private ImapClient _emailClient = null;
        private EmailConnector _emailConnector = null;
        private IUnitOfWork _unitOfWork = null;

        public IMAPEmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
            _unitOfWork = unitOfWork;
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

            if (!_emailClient.Inbox.IsOpen)
            {
                _emailClient.Inbox.Open(FolderAccess.ReadOnly);
            }

            var folders = GetFolders(_emailClient.Inbox);
            folders.Add(_emailClient.Inbox);

            while (true)
            {
                foreach (var folder in folders)
                {
                    if (!folder.IsOpen)
                    {
                        folder.Open(MailKit.FolderAccess.ReadOnly);
                    }

                    var folderEntity = await GetOrCreateFolder(_emailConnector.EmailConnectorId, folder.FullName, folder.Name);

                    Console.WriteLine($"Current Folder: {folderEntity.FolderPath}");

                    List<MailKit.UniqueId> ids = null;
                    if (_emailClient.Inbox != null)
                    {
                        var range = new UniqueIdRange(new UniqueId((uint)1), UniqueId.MaxValue);
                        ids = folder.Search(MailKit.Search.SearchQuery.Uids(range)).Where(x => x.Id > (uint)1).OrderBy(x => x.Id).Take(10).ToList();
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

                Thread.Sleep(60000);
            }
        }

        private List<IMailFolder> GetFolders(IMailFolder rootFolder)
        {
            var folders = new List<IMailFolder>();

            var subFolders = rootFolder.GetSubfolders();

            if (subFolders.Count > 0)
            {
                foreach (var folder in subFolders)
                {
                    folders.AddRange(GetFolders(folder));
                }
            }

            return folders;
        }

        private async Task<EmailFolderConfigurationModel> GetOrCreateFolder(Guid emailconnectorId, string folderName, string folderPath)
        {
            var folder = await _unitOfWork.EmailFolderRepository.GetEmailFolder(emailconnectorId, folderPath);

            if (folder == null)
            {
                folder = await _unitOfWork.EmailFolderRepository.CreateEmailFolder(emailconnectorId, folderPath, folderName);
            }

            return folder;
        }
    }
}