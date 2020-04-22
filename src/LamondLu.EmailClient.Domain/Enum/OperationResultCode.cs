using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Enum
{
    public enum OperationResultCode
    {
        Success = 0,

        UnexpectedError = 1,

        //Email Connector Operation 10000-10100
        EmailConnector_Duplicated = 10000


    }
}
