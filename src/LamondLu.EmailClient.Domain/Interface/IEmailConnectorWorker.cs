using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Interface
{
    public delegate void EmailReceived();

    public interface IEmailConnectorWorker
    {
        RulePipeline Pipeline { get; }

        event EmailReceived EmailReceived;

        Task<bool> Connect();

        Task Listen();
    }
}
