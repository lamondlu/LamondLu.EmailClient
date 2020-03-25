using System;

namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IUnitOfWork
    {
        void SaveAsync();
    }
}
