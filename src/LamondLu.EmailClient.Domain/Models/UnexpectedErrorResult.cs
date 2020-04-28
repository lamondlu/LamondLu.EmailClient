using LamondLu.EmailClient.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Models
{
    public class UnexpectedErrorResult : OperationResult
    {
        public UnexpectedErrorResult(string message) : base(false, OperationResultCode.UnexpectedError, message)
        {
        }
    }
}
