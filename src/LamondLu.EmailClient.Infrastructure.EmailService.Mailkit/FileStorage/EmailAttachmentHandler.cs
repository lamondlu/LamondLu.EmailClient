using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.Extensions;
using MimeKit;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public class EmailAttachmentHandler : IEmailAttachmentHandler
    {
        public EmailAttachmentHandler(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        private IFileStorage _fileStorage = null;

        public async Task<List<AddEmailAttachmentModel>> SaveAttachment(Guid emailId, MimeMessage mail)
        {
            var result = new List<AddEmailAttachmentModel>();
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

                        await _fileStorage.Upload(emailId, newFileName, ms);

                        result.Add(new AddEmailAttachmentModel
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

            return result;
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