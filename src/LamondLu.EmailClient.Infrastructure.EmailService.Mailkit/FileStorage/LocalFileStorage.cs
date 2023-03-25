using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public class LocalFileStorage : IFileStorage
    {
        public void Upload(Guid emailId, string fileName, MemoryStream stream)
        {
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}/attachments/{emailId}/{fileName}", FileMode.Create))
            {
                stream.WriteTo(fs);
                fs.Flush();
            }
        }
    }
}