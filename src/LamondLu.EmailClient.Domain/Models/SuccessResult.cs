namespace LamondLu.EmailClient.Domain.Models
{
    public class SuccessResult : OperationResult
    {
        public SuccessResult() : base(true, Enum.OperationResultCode.Success, "Success")
        {

        }
    }
}
