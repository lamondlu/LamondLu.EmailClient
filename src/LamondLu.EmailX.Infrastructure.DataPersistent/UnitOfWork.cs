using Dapper;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Infrastructure.DataPersistent.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmailConnectorRepository _emailConnectorRepository = null;

        private IEmailFolderRepository _emailFolderRepository = null;

        private IEmailRepository _emailRepository = null;

        private IEmailAttachmentRepository _emailAttachmentRepository = null;

        private MySqlConnection _connection = null;
        private DbSetting _dbSetting = null;
        private DapperDbContext _dbContext = null;

        public UnitOfWork(IOptions<DbSetting> optionsAccessor)
        {
            _dbSetting = optionsAccessor.Value;
            _connection = new MySqlConnection(_dbSetting.ConnectionString);
            _dbContext = new DapperDbContext(_connection, _dbSetting.TimeOut);
        }

        public void CreateDatabaseIfNotExisted()
        {
            var databaseName = _connection.Database;
            try
            {
                var sql = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}'";
                var result = _connection.ExecuteScalar(sql);

                TryToUpgradeIfNeed();
            }
            catch (MySqlException ex)
            {
                if ((ex.Message ?? "").Contains($"Unknown database '{databaseName}'"))
                {
                    Console.WriteLine("Database is not existed. Try to initialize the database.");
                    CreateDatabase(databaseName);
                }
                else
                {
                    Console.WriteLine("Error: Initialization failed. Please check the database connection.");
                }
            }
        }

        private void TryToUpgradeIfNeed()
        {
            var lastMigration = _connection.QueryFirstOrDefault<string>("SELECT MigrationId FROM Migrations ORDER BY MigrationId DESC LIMIT 1");

            var assembly = Assembly.Load("LamondLu.EmailX.Client");
            var sqlFiles = assembly.GetManifestResourceNames().ToList();

            if (!sqlFiles.Contains(lastMigration))
            {
                sqlFiles.Add(lastMigration);
            }

            sqlFiles = sqlFiles.OrderBy(e => e).SkipWhile(e => e != lastMigration).Skip(1).ToList();
            RunMigration(_connection, sqlFiles.ToList(), _connection.Database);
        }

        private void RunMigration(MySqlConnection connection, List<string> sqlFiles, string dbName)
        {
            var assembly = Assembly.Load("LamondLu.EmailX.Client");

            foreach (var sqlFile in sqlFiles)
            {
                using (var sr = new StreamReader(assembly.GetManifestResourceStream(sqlFile)))
                {
                    var sql = sr.ReadToEnd().Replace("$DB_NAME", dbName);
                    Console.WriteLine($"Executing sql: {sqlFile}");
                    connection.Execute(sql);
                }
            }
        }

        private void CreateDatabase(string dbName)
        {
            var assembly = Assembly.Load("LamondLu.EmailX.Client");
            var sqlFiles = assembly.GetManifestResourceNames();

            using (var connection = new MySqlConnection(_dbSetting.ConnectionString.Replace(dbName, "mysql")))
            {
                RunMigration(connection, sqlFiles.ToList(), dbName);
            }
        }

        public IEmailRepository EmailRepository
        {
            get
            {
                if (_emailRepository == null)
                {
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

        public IEmailAttachmentRepository EmailAttachmentRepository
        {
            get
            {
                if (_emailAttachmentRepository == null)
                {
                    return new EmailAttachmentRepository(_dbContext);
                }

                return _emailAttachmentRepository;
            }
        }

        public IEmailRecipientRepository EmailRecipientRepository
        {
            get
            {
                return new EmailRecipientRepository(_dbContext);
            }
        }

        public async Task<DbOperationResult> SaveAsync()
        {
            return await _dbContext.SubmitAsync();
        }
    }
}
