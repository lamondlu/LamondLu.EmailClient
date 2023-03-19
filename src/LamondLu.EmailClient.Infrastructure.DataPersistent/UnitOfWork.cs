using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.Models;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmailConnectorRepository _emailConnectorRepository = null;
        private IEmailFolderRepository _emailFolderRepository = null;

        private IEmailRepository _emailRepository = null;

        private MySqlConnection _connection = null;
        private DbSetting _dbSetting = null;
        private DapperDbContext _dbContext = null;

        public UnitOfWork(IOptions<DbSetting> optionsAccessor)
        {
            _dbSetting = optionsAccessor.Value;
            _connection = new MySqlConnection(_dbSetting.ConnectionString);
            _dbContext = new DapperDbContext(_connection, _dbSetting.TimeOut);
        }

        public IEmailRepository EmailRepository{
            get{
                if(_emailRepository  == null){
                    return new EmailRepository(_dbContext);
                }

                return _emailRepository;
            }
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

        public IEmailFolderRepository EmailFolderRepository
        {
            get
            {
                if (_emailFolderRepository == null)
                {
                    return new EmailFolderRepository(_dbContext);
                }

                return _emailFolderRepository;
            }
        }

        public async Task<DbOperationResult> SaveAsync()
        {
            return await _dbContext.SubmitAsync();
        }
    }
}
