using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.FileStorage
{
    /// <summary>
    /// A interface to handle file storage
    /// </summary>
    public interface IFileStorage
    {
        Task Upload(Guid emailId, string fileName, MemoryStream stream);
    }
}