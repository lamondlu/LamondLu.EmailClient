using System;
using System.IO;
using System.Linq;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    /// <summary>
    /// Use local file storage to store inline images
    /// </summary>
    public class LocalInlineImageHandler : IInlineImageHandler
    {
        public string PopulateInlineImages(MimeMessage newMessage, Guid emailId)
        {
            var body = string.Empty;

            if (!string.IsNullOrEmpty(newMessage.HtmlBody))
            {
                body = newMessage.HtmlBody;

                foreach (var entity in newMessage.BodyParts)
                {
                    if (entity is MimePart)
                    {
                        var att = entity as MimePart;
                        if (att.ContentId != null && att.Content != null && (body.IndexOf("cid:" + att.ContentId) > -1))
                        {
                            var fileType = "png";
                            if (att.ContentType.MediaType == "image" && !string.IsNullOrEmpty(att.FileName) && att.FileName.Contains("."))
                            {
                                fileType = att.FileName.Split(".").Last();
                            }
                            //byte[] b;
                            using (var mem = new MemoryStream())
                            {
                                att.Content.DecodeTo(mem);
                                mem.Position = 0;

                                var attachmentFolder = new DirectoryInfo($"{Directory.GetCurrentDirectory()}/images");
                                if (!attachmentFolder.Exists)
                                {
                                    attachmentFolder.Create();
                                }

                                var fileName = $"{DateTime.Now.Ticks}.{fileType}";

                                using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}/images/{fileName}", FileMode.Create))
                                {
                                    mem.WriteTo(fs);
                                    fs.Flush();
                                }

                                var fileFullUrl = $"https://filestorage/emails/{emailId}/attachments/{fileName}";
                                body = body.Replace("cid:" + att.ContentId, fileFullUrl);
                            }
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(newMessage.TextBody))
            {
                body = newMessage.TextBody.Replace("\r\n", "<br /><br />");
            }
            else
            {
                body = "<div>&nbsp;</div>";
            }

            return body;
        }
    }
}