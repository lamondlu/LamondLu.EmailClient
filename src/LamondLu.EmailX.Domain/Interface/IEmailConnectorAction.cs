using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEmailConnectorAction
    {
        Task StopConnector(Guid emailConnectorId);

        Task StartConnector(Guid emailConnectorId);
    }
}