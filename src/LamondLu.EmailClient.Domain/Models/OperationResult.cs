using LamondLu.EmailClient.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Models
{
    public abstract class OperationResult
    {
        public OperationResult(bool success, OperationResultCode code, string message)
        {
            Success = success;

            if (!Success)
            {
                Code = code;
                Message = message;
            }
        }

        public bool Success { get; private set; }

        public OperationResultCode Code { get; private set; }

        public string Message { get; private set; }

        public static SuccessResult SuccessResult = new SuccessResult();
    }
}
