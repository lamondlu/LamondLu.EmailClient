using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Models;

namespace LamondLu.EmailClient.Domain.Results
{
    public class DuplicateEmailConnectorResult : OperationResult
    {
        public DuplicateEmailConnectorResult(string message = "The email connector is existed in the system.") : base(false, OperationResultCode.EmailConnector_Duplicated, message)
        {
        }
    }
}
