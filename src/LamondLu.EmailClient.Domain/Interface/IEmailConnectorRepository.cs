using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorRepository
    {
        Task<List<EmailConnectorConfigViewModel>> GetEmailConnectors();

        Task AddEmailConnector(EmailConnector emailConnector);
    }
}
