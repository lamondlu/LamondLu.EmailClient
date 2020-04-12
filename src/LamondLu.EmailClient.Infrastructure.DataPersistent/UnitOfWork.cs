using LamondLu.EmailClient.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
