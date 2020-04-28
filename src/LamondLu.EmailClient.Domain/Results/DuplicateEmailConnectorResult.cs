using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Results
{
    public class DuplicateEmailConnectorResult : OperationResult
    {
        public DuplicateEmailConnectorResult(string message = "The email connector is existed in the system.") : base(false, OperationResultCode.EmailConnector_Duplicated, message)
        {
        }
    }
}
