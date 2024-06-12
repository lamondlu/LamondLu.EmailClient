namespace LamondLu.EmailX.Domain.Models
{
    public class SuccessResult : OperationResult
    {
        public SuccessResult() : base(true, Enum.OperationResultCode.Success, "Success")
        {

        }
    }
}
