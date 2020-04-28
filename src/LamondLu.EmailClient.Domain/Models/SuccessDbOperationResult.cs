using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Models
{
    public class SuccessDbOperationResult : DbOperationResult
    {
        public SuccessDbOperationResult() : base(true)
        {
        }
    }
}
