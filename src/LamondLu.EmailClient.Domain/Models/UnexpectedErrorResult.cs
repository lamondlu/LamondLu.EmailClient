using LamondLu.EmailClient.Domain.Enum;

namespace LamondLu.EmailClient.Domain.Models
{
    public class UnexpectedErrorResult : OperationResult
    {
        public UnexpectedErrorResult(string message) : base(false, OperationResultCode.UnexpectedError, message)
        {
        }
    }
}
