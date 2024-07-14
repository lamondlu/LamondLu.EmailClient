using System.Threading.Tasks;
using LamondLu.EmailX.Domain.DTOs;

namespace LamondLu.EmailX.Domain.Interface
{
    public delegate void EmailReceived(Email email);

    public interface IEmailConnectorWorker
    {
        RulePipeline Pipeline { get; }

        event EmailReceived EmailReceived;

        Task<bool> Connect();

        Task<bool> Disconnect();

        Task Listen();
    }
}
