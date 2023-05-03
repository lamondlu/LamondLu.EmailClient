using System;

namespace LamondLu.EmailX.Domain.Models
{
    public class ErrorDbOperationResult : DbOperationResult
    {
        public ErrorDbOperationResult(Exception error) : base(false, error)
        {
        }
    }
}
