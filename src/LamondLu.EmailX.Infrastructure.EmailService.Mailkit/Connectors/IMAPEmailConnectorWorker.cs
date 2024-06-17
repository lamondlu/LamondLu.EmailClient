using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnectorWorker : IEmailConnectorWorker
    {
        private ImapClient _emailClient = null;

        private EmailConnector _emailConnector = null;

        private IUnitOfWork _unitOfWork = null;

        private IInlineImageHandler _inlineImageHandler = null;

        private IEmailAttachmentHandler _emailAttachmentHandler = null;

        public IMAPEmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler, IEmailAttachmentHandler emailAttachmentHandler)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
            _unitOfWork = unitOfWork;
            _inlineImageHandler = inlineImageHandler;
            _emailAttachmentHandler = emailAttachmentHandler;
        }

        public RulePipeline Pipeline { get; }

        public event EmailReceived EmailReceived;

        public async Task<bool> Connect()
        {
            try
            {
                _emailClient = new ImapClient();

                await _emailClient.ConnectAsync(_emailConnector.Server.IMAPServer, _emailConnector.Server.IMAPPort.Value, true);
                _emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await _emailClient.AuthenticateAsync(_emailConnector.UserName, _emailConnector.Password);

                // if (_emailConnector.Server.IsNetEase)
                // {
                //     //for netease box, there need some extra work
                //     await SpeicalBox();
                // }

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
                        folder.Open(FolderAccess.ReadOnly);
                    }

                    var folderEntity = await GetOrCreateFolder(_emailConnector.EmailConnectorId, folder.FullName, folder.Name);

                    Console.WriteLine($"Current Folder: {folderEntity.FolderPath}");


                    List<UniqueId> ids = null;
                    if (folder != null)
                    {
                        var start = new UniqueId(folder.UidValidity, folderEntity.LastEmailId + 1);
                        var end = new UniqueId(folder.UidValidity, UniqueId.MaxValue.Id);

                        var range = new UniqueIdRange(start, end);
                        ids = _emailClient.Inbox.Search(SearchQuery.Uids(range)).Take(100).ToList();
                    }

                    if (ids.Count != 0)
                    {
                        var items = folder.Fetch(ids, MessageSummaryItems.Flags);

                        foreach (var emailId in ids)
                        {
                            var email = folder.GetMessage(emailId);
                            Console.WriteLine($"[{email.Date}] {email.Subject}");

                            await SaveMessage(email, _emailConnector.EmailConnectorId, folderEntity.EmailFolderId, emailId, items.FirstOrDefault(p => p.UniqueId == emailId).Flags.Value.HasFlag(MessageFlags.Seen));
                        }
                    }
                }

                Thread.Sleep(30000);
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

        private async Task SaveMessage(MimeMessage mail, Guid emailConnectorId, Guid folderId, UniqueId emailId, bool isRead)
        {
            var dup = await _unitOfWork.EmailRepository.MessageIdExisted(mail.MessageId);

            if (dup)
            {
                Console.WriteLine($"The email id {mail.MessageId} is existed. System skip it.");
                return;
            }

            var email = await SaveEmail(mail, emailConnectorId, folderId, emailId, isRead);

            await _unitOfWork.EmailRepository.SaveEmailBody(email.EmailId, mail.TextBody, _inlineImageHandler.PopulateInlineImages(mail, email.EmailId));
            await SaveAttachment(email.EmailId, mail);
            await _unitOfWork.EmailFolderRepository.RecordFolderProcess(folderId, emailId.Id, emailId.Validity);
            await _unitOfWork.SaveAsync();

            if (EmailReceived != null)
            {
                EmailReceived(email);
            }
        }

        private async Task<AddEmailModel> SaveEmail(MimeMessage mail, Guid emailConnectorId, Guid folderId, UniqueId emailId, bool isRead)
        {
            var newEmail = new AddEmailModel
            {
                EmailConnectorId = emailConnectorId,
                EmailFolderId = folderId,
                Subject = mail.Subject,
                Sender = mail.From?.Mailboxes?.FirstOrDefault()?.Address,
                ReceivedDate = mail.Date.Date,
                MessageId = mail.MessageId,
                Id = emailId.Id,
                Validity = emailId.Validity,
                IsRead = isRead
            };

            await _unitOfWork.EmailRepository.SaveNewEmail(newEmail);

            return newEmail;
        }

        private async Task SaveAttachment(Guid emailId, MimeMessage mail)
        {
            var result = await _emailAttachmentHandler.SaveAttachment(emailId, mail);

            foreach (var item in result)
            {
                await _unitOfWork.EmailAttachmentRepository.AddEmailAttachment(item);
            }
        }
    }
}