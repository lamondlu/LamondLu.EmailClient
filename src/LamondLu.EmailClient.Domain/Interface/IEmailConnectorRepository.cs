using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorRepository
    {
        List<EmailConnectorConfigViewModel> GetEmailConnectors();
    }
}
