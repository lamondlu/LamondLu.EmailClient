using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailConnectorRepository : IEmailConnectorRepository
    {
        public List<EmailConnectorConfigViewModel> GetEmailConnectors()
        {
            throw new NotImplementedException();
        }
    }
}
