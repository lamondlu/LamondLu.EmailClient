using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailAttachmentRepository
    {
        Task AddEmailAttachment(AddEmailAttachmentModel dto);
    }
}