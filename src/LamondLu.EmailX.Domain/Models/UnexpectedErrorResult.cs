using LamondLu.EmailX.Domain.Enum;

namespace LamondLu.EmailX.Domain.Models
{
    public class UnexpectedErrorResult : OperationResult
    {
        public UnexpectedErrorResult(string message) : base(false, OperationResultCode.UnexpectedError, message)
        {
        }
    }
}
