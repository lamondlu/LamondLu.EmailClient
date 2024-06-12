using LamondLu.EmailX.Domain.Enum;
using LamondLu.EmailX.Domain.Models;

namespace LamondLu.EmailX.Domain.Results
{
    public class DuplicateEmailConnectorResult : OperationResult
    {
        public DuplicateEmailConnectorResult(string message = "The email connector is existed in the system.") : base(false, OperationResultCode.EmailConnector_Duplicated, message)
        {
        }
    }
}
