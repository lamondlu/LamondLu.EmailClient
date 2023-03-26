using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.Extensions;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage;
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

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class IMAPEmailConnectorWorker : IEmailConnectorWorker
    {
        private ImapClient _emailClient = null;
        private EmailConnector _emailConnector = null;
        private IUnitOfWork _unitOfWork = null;
        private IInlineImageHandler _inlineImageHandler = null;

        private IFileStorage _fileStorage = null;

        public IMAPEmailConnectorWorker(EmailConnector emailConnector, IRuleProcessorFactory ruleProcessorFactory, IUnitOfWork unitOfWork, IInlineImageHandler inlineImageHandler, IFileStorage fileStorage)
        {
            Pipeline = new RulePipeline(emailConnector.Rules, ruleProcessorFactory, unitOfWork);
            _emailConnector = emailConnector;
            _unitOfWork = unitOfWork;
            _inlineImageHandler = inlineImageHandler;
            _fileStorage = fileStorage;
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
                        folder.Open(FolderAccess.ReadOnly);
                    }

                    var folderEntity = await GetOrCreateFolder(_emailConnector.EmailConnectorId, folder.FullName, folder.Name);

                    Console.WriteLine($"Current Folder: {folderEntity.FolderPath}");


                    List<UniqueId> ids = null;
                    if (_emailClient.Inbox != null)
                    {
                        var start = new UniqueId(folderEntity.LastValidityId, folderEntity.LastEmailId);
                        var end = new UniqueId(folderEntity.LastValidityId, folderEntity.LastEmailId + (uint)_emailClient.Inbox.Count);

                        var range = new UniqueIdRange(start, end);
                        ids = _emailClient.Inbox.Search(SearchQuery.Uids(range)).ToList();
                    }

                    if (ids.Count != 0)
                    {
                        foreach (var emailId in ids)
                        {
                            var email = folder.GetMessage(emailId);
                            Console.WriteLine($"[{email.Date}] {email.Subject}");

                            await SaveMessage(email, _emailConnector.EmailConnectorId, folderEntity.FolderId, emailId);
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

        private async Task SaveMessage(MimeMessage mail, Guid emailConnectorId, Guid folderId, UniqueId emailId)
        {
            //save message

            var dup = await _unitOfWork.EmailRepository.MessageIdExisted(mail.MessageId);

            if (dup)
            {
                Console.WriteLine($"The email id {mail.MessageId} is existed. System skip it.");
                return;
            }

            var email = await SaveEmail(mail, emailConnectorId, folderId, emailId);
            await SaveEmailBody(email.EmailId, mail.TextBody, _inlineImageHandler.PopulateInlineImages(mail));
            await SaveAttachment(email.EmailId, mail);
            await _unitOfWork.EmailFolderRepository.RecordFolderProcess(folderId, emailId.Id, emailId.Validity);
            await _unitOfWork.SaveAsync();

            if (EmailReceived != null)
            {
                EmailReceived(email);
            }
        }

        private async Task<AddEmailModel> SaveEmail(MimeMessage mail, Guid emailConnectorId, Guid folderId, UniqueId emailId)
        {
            var newEmail = new AddEmailModel();
            newEmail.EmailConnectorId = emailConnectorId;
            newEmail.EmailFolderId = folderId;
            newEmail.Subject = mail.Subject;
            newEmail.Sender = mail.From?.Mailboxes?.FirstOrDefault()?.Address;
            newEmail.ReceivedDate = mail.Date.Date;
            newEmail.MessageId = mail.MessageId;
            newEmail.Id = emailId.Id;
            newEmail.Validity = emailId.Validity;

            await _unitOfWork.EmailRepository.SaveNewEmail(newEmail);

            return newEmail;
        }

        private async Task SaveEmailBody(Guid emailId, string body, string htmlBody)
        {
            await _unitOfWork.EmailRepository.SaveEmailBody(emailId, body, htmlBody);
        }

        private async Task SaveAttachment(Guid emailId, MimeMessage mail)
        {
            var allAttachments = new Dictionary<MemoryStream, string>();
            List<int> fileHashCodeList = new List<int>();
            var messagePart_attachments = mail.Attachments.OfType<MessagePart>();
            if (messagePart_attachments != null && messagePart_attachments.Any())
            {
                foreach (var item in messagePart_attachments)
                {
                    var files = item.Message?.Attachments?.ToList();
                    if (files != null && files.Any())
                    {
                        foreach (var file in files)
                        {
                            if (fileHashCodeList.Contains(file.GetHashCode()))
                            {
                                continue;
                            }
                            else
                            {

                                var fileName = file.ContentDisposition?.FileName ?? file.ContentType.Name;
                                fileName = fileName.GetSafeFileName();
                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    MemoryStream attachment_file_ms = new MemoryStream();
                                    if (file is MimePart)
                                    {
                                        var file_mimiPart = file as MimePart;
                                        file_mimiPart.Content.DecodeTo(attachment_file_ms);
                                    }
                                    else
                                    {
                                        file.WriteTo(attachment_file_ms);
                                    }

                                    allAttachments.Add(attachment_file_ms, fileName);
                                    fileHashCodeList.Add(file.GetHashCode());
                                }
                            }
                        }
                    }

                    if (fileHashCodeList.Contains(item.GetHashCode()))
                    {
                        continue;
                    }
                    else
                    {
                        MemoryStream attachment_ms = new MemoryStream();
                        item.WriteTo(attachment_ms);
                        var attachment_filename = (item.Message.Subject ?? "attach_msg") + ".eml";
                        allAttachments.Add(attachment_ms, attachment_filename.GetSafeFileName());
                        fileHashCodeList.Add(item.GetHashCode());
                    }
                }
            }
            var normal_attachments = mail.Attachments.OfType<MimePart>().Where(p => !string.IsNullOrEmpty(p.FileName)).ToList();
            if (normal_attachments != null && normal_attachments.Any())
            {
                foreach (var normal_item in normal_attachments)
                {
                    if (fileHashCodeList.Contains(normal_item.GetHashCode()))
                    {
                        continue;
                    }
                    else
                    {
                        MemoryStream normal_ms = new MemoryStream();
                        normal_item.Content.DecodeTo(normal_ms);
                        allAttachments.Add(normal_ms, normal_item.FileName.GetSafeFileName());
                        fileHashCodeList.Add(normal_item.GetHashCode());
                    }

                }
            }
            var attachments = mail.BodyParts.OfType<MimePart>().Where(p => !string.IsNullOrEmpty(p.FileName)).ToList();// email.Attachments;
            if (attachments != null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                    if (fileHashCodeList.Contains(item.GetHashCode()))
                    {
                        continue;
                    }
                    else
                    {
                        MemoryStream normal_ms = new MemoryStream();
                        item.Content.DecodeTo(normal_ms);
                        allAttachments.Add(normal_ms, item.FileName.GetSafeFileName());
                        fileHashCodeList.Add(item.GetHashCode());
                    }

                }
            }

            if (allAttachments.Count() > 0)
            {
                Dictionary<string, int> fileNameList = new Dictionary<string, int>();
                //var specialNameCount = 0;
                foreach (var item in allAttachments)
                {

                    if (item.Key.Length == 0)
                    {
                        continue;
                    }

                    var fileName = item.Value?.Replace("‎", "");
                    var newFileName = string.Empty;
                    if (fileNameList.ContainsKey(fileName.ToLower()))
                    {
                        var number = fileNameList[fileName.ToLower()] + 1;

                        var fileType = fileName.Split(".").Last();
                        if (fileName == fileType) //it means there is no extension name
                        {
                            fileName = $"{fileName}_{number}_";
                            newFileName = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            fileName = fileName.Insert(fileName.LastIndexOf($".{fileType}"), $"_{number}_");
                            newFileName = $"{Guid.NewGuid()}.{fileType}";
                        }
                        fileNameList[item.Value.ToLower()] = number;
                    }
                    else
                    {
                        var fileType = fileName.Split(".").Last();
                        if (fileName == fileType) //it means there is no extension name
                        {
                            newFileName = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            newFileName = $"{Guid.NewGuid()}.{fileType}";
                        }

                        fileNameList.Add(item.Value.ToLower(), 0);
                    }
                    var ms = item.Key;
                    long length = ms.Length;
                    if (length > 0)
                    {
                        ms.Position = 0;

                        _fileStorage.Upload(emailId, newFileName, ms);

                        await _unitOfWork.EmailAttachmentRepository.AddEmailAttachment(new AddEmailAttachmentModel
                        {
                            EmailAttachmentId = Guid.NewGuid(),
                            SourceFileName = fileName,
                            FileSize = GetFileSize(length),
                            FileName = newFileName,
                            EmailId = emailId
                        });
                    }
                }

                allAttachments.Clear();
                fileHashCodeList.Clear();
                fileNameList.Clear();
            }
        }

        public string GetFileSize(long file_length)
        {
            try
            {
                if (file_length < 1024)
                {
                    return file_length + "b";
                }
                else if (file_length < 1024 * 1024)
                {
                    return ((double)file_length / 1024.0).ToString("0.00") + "kb";
                }
                else if (file_length < 1024 * 1024 * 1024)
                {
                    return ((double)file_length / (1024.0 * 1024.0)).ToString("0.00") + "M";
                }
                else
                {
                    return ((double)file_length / (1024.0 * 1024.0)).ToString("0") + "M";
                }
            }
            catch
            {
                return "";
            }
        }
    }


}