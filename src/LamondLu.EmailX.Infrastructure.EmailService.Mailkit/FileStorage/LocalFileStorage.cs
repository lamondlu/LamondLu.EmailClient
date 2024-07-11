using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    /// <summary>
    /// Use local file storage to store email attachments
    /// </summary>
    public class LocalFileStorage : IFileStorage
    {
        public async Task Upload(Guid emailId, string fileName, MemoryStream stream)
        {
            var emailFolder = new DirectoryInfo($"{Directory.GetCurrentDirectory()}/attachments/{emailId}");

            if (!emailFolder.Exists)
            {
                emailFolder.Create();
            }

            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}/attachments/{emailId}/{fileName}", FileMode.Create))
            {
                stream.WriteTo(fs);
                await fs.FlushAsync();
            }
        }

        public async Task<Stream> Download(Guid emailId, string fileName)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}/attachments/{emailId}/{fileName}";

            if (!File.Exists(filePath))
            {
                return null;
            }

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
    }
}