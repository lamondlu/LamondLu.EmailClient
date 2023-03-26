using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailAttachmentRepository
    {
        Task AddEmailAttachment(AddEmailAttachmentModel dto);
    }
}