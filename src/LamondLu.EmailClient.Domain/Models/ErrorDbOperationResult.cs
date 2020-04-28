using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Models
{
    public class ErrorDbOperationResult : DbOperationResult
    {
        public ErrorDbOperationResult(Exception error) : base(false, error)
        {
        }
    }
}
