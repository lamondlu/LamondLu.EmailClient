using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit.FileStorage
{
    public interface IFileStorage
    {
        void Upload(Guid emailId, string fileName, MemoryStream stream);
    }
}