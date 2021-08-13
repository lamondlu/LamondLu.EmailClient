namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IEmailConnectorWorkerFactory
    {
        IEmailConnectorWorker Build(EmailConnector emailConnector,
            IRuleProcessorFactory ruleProcessorFactory,
            IUnitOfWork unitOfWork);
    }
}