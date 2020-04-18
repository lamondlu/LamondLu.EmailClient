using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
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
