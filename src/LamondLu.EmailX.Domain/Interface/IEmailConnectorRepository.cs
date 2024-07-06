using LamondLu.EmailX.Domain.Enum;
using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailConnectorRepository
    {
        Task<List<EmailConnectorConfigViewModel>> GetEmailConnectorConfigs();

        Task<EmailConnectorConfigViewModel> GetEmailConnectorConfig(Guid emailConnectorId);

        Task AddEmailConnector(EmailConnector emailConnector);

        Task<bool> CheckDuplicated(string emailAddress, string name, Guid emailConnectorId);

        Task<EmailConnector> GetEmailConnector(Guid emailConnectorId);

        Task<List<EmailConnectorStatusViewModel>> GetEmailConnectorStatuses();

        Task UpdateEmailConnectorStatus(Guid emailConnectorId, EmailConnectorStatus status);
    }
}
