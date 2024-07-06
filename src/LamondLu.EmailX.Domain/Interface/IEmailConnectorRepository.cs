using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailConnectorRepository
    {
        Task<List<EmailConnectorConfigViewModel>> GetEmailConnectors();

        Task AddEmailConnector(EmailConnector emailConnector);

        Task<bool> CheckDuplicated(string emailAddress, string name, Guid emailConnectorId);

        Task<EmailConnector> GetEmailConnector(Guid emailConnectorId);

        Task<List<EmailConnectorStatusViewModel>> GetEmailConnectorStatuses();
    }
}
