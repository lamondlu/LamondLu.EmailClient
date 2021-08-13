using Dapper;
using LamondLu.EmailClient.Domain.Models;
using LamondLu.EmailClient.Infrastructure.DataPersistent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class DapperDbContext : IDisposable
    {
        private MySqlConnection _connection = null;
        private List<DapperCommand> _commands = new List<DapperCommand>();
        private int _commandTimeout = 10;

        public DapperDbContext(MySqlConnection connection, int timeout)
        {
            _connection = connection;
        }

        public async Task Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            await Task.Run(() =>
            {
                _commands.Add(new DapperCommand(sql, param, commandType));
            });
        }

        public async Task<DbOperationResult> SubmitAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            var tran = await _connection.BeginTransactionAsync();

            try
            {
                foreach (var command in _commands)
                {
                    await _connection.ExecuteAsync(command.Sql, command.Parameters, tran, _commandTimeout, command.CommandType);
                }

                tran.Commit();
                return new SuccessDbOperationResult();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new ErrorDbOperationResult(ex);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _connection.QueryAsync<T>(sql, param, commandTimeout: _commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {

                ThrowErrorMessage(ex);
            }

            return null;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout, commandType: commandType);

            }
            catch (Exception ex)
            {

                ThrowErrorMessage(ex);
            }

            return default(T);
        }

        public async Task<T> ExecuteScalar<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _connection.ExecuteScalarAsync<T>(sql, param, commandTimeout: _commandTimeout, commandType: commandType);
            }
            catch (Exception ex)
            {

                ThrowErrorMessage(ex);
            }

            return default(T);
        }

        private void ThrowErrorMessage(Exception ex)
        {
            if (ex.InnerException == null)
            {
                throw new Exception(ex.Message);
            }

            throw new Exception(ex.InnerException.Message);
        }

        public void Dispose()
        {
            _connection.Close();
            _connection = null;
        }
    }
}
