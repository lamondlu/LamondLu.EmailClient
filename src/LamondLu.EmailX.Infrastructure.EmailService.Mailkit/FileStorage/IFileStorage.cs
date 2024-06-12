using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    public interface IFileStorage
    {
        Task Upload(Guid emailId, string fileName, MemoryStream stream);
    }
}