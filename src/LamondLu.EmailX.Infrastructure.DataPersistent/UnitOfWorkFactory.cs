using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using Microsoft.Extensions.Options;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IOptions<DbSetting> _optionsAccessor = null;

        public UnitOfWorkFactory(IOptions<DbSetting> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_optionsAccessor);
        }
    }
}
