using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Models
{
    public class SuccessResult : OperationResult
    {
        public SuccessResult() : base(true, Enum.OperationResultCode.Success, "Success")
        {

        }
    }
}
