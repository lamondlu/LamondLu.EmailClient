using Dapper;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.Models;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmailConnectorRepository _emailConnectorRepository = null;
        private MySqlConnection _connection = null;
        private DbSetting _dbSetting = null;
        private DapperDbContext _dbContext = null;

        public UnitOfWork(IOptions<DbSetting> optionsAccessor)
        {
            _dbSetting = optionsAccessor.Value;
            _connection = new MySqlConnection(_dbSetting.ConnectionString);
            _dbContext = new DapperDbContext(_connection, _dbSetting.Timeout);
        }

        public IEmailConnectorRepository EmailConnectorRepository
        {
            get
            {
                if (_emailConnectorRepository == null)
                {
                    return new EmailConnectorRepository(_dbContext);
                }

                return _emailConnectorRepository;
            }
        }

        public async Task<DbOperationResult> SaveAsync()
        {
            return await _dbContext.SubmitAsync();
        }
    }
}
