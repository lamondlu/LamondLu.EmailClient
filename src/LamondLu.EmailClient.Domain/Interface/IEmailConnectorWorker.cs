using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;

namespace LamondLu.EmailClient.Domain.Interface
{
    public delegate void EmailReceived(AddEmailModel email);

    public interface IEmailConnectorWorker
    {
        RulePipeline Pipeline { get; }

        event EmailReceived EmailReceived;

        Task<bool> Connect();

        Task Listen();
    }
}
