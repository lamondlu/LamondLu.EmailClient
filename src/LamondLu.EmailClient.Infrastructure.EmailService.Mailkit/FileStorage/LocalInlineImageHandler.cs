using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public class LocalInlineImageHandler : IInlineImageHandler
    {
        public string PopulateInlineImages(MimeMessage newMessage)
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

                                using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}/images/{DateTime.Now.Ticks}.{fileType}", FileMode.Create))
                                {
                                    mem.WriteTo(fs);
                                    fs.Flush();
                                }
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