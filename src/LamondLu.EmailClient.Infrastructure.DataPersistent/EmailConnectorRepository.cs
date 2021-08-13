using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailConnectorRepository : IEmailConnectorRepository
    {
        private DapperDbContext _context = null;

        public EmailConnectorRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmailConnectorConfigViewModel>> GetEmailConnectors()
        {
            var sql = "SELECT * FROM EmailConnector WHERE IsDeleted = 0";
            var result = await _context.QueryAsync<EmailConnectorConfigViewModel>(sql);

            return result.ToList();
        }

        public async Task AddEmailConnector(EmailConnector emailConnector)
        {
            var sql = @"INSERT INTO EmailConnector(EmailConnectorId, 
                    Name, 
                    EmailAddress, 
                    UserName, 
                    Password, 
                    Status, 
                    IP, 
                    Port, 
                    EnableSSL, 
                    Description, Type) 
                    VALUES(@EmailConnectorId, @Name, @EmailAddress, @UserName, @Password, @Status, @IP, @Port, @EnableSSL, @Description, @Type)";

            await _context.Execute(sql, new
            {
                emailConnector.EmailConnectorId,
                emailConnector.Name,
                emailConnector,
                emailConnector.Password,
                emailConnector.Status,
                IP = emailConnector.Server.Server,
                emailConnector.Server.Port,
                emailConnector.Server.EnableSSL,
                emailConnector.Description,
                emailConnector.UserName,
                emailConnector.Type
            });
        }

        public async Task<bool> CheckDuplicated(string emailAddress, string name, Guid emailConnectorId)
        {
            var sql = "SELECT COUNT(*) FROM EmailConnector WHERE IsDeleted=0 AND Name=@name AND EmailConnectorId<>@emailConnectorId";
            var count = await _context.ExecuteScalar<int>(sql, new { name, emailConnectorId });
            return count > 0;
        }

        public Task<EmailConnector> GetEmailConnector(Guid emailConnectorId)
        {
            throw new NotImplementedException();
        }
    }
}
