using System;

namespace LamondLu.EmailClient.Domain.Models
{
    public abstract class DbOperationResult
    {
        public DbOperationResult(bool success)
        {
            this.Success = success;
        }

        public DbOperationResult(bool success, Exception error)
        {
            this.Success = success;
            this.Error = error;
        }

        public bool Success { get; private set; }

        public Exception Error { get; private set; }
    }
}
