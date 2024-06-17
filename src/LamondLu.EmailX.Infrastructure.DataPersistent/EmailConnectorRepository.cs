using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.DataPersistent
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
                    SMTPServer,
                    SMTPPort,
                    IMAPServer,
                    IMAPPort,
                    POP3Server,
                    POP3Port,
                    EnableSSL, 
                    Description, Type) 
                    VALUES(@EmailConnectorId, @Name, @EmailAddress, @UserName, @Password, @Status, @SMTPServer, @SMTPPort, @IMAPServer, @IMAPPort, @POP3Server, @POP3Port, @EnableSSL, @Description, @Type)";

            await _context.Execute(sql, new
            {
                emailConnector.EmailConnectorId,
                emailConnector.Name,
                emailConnector,
                emailConnector.Password,
                emailConnector.Status,
                emailConnector.Server.SMTPServer,
                emailConnector.Server.SMTPPort,
                emailConnector.Server.IMAPServer,
                emailConnector.Server.IMAPPort,
                emailConnector.Server.POP3Server,
                emailConnector.Server.POP3Port,
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

        public async Task<EmailConnector> GetEmailConnector(Guid emailConnectorId)
        {
            var sql = "SELECT * FROM EmailConnector WHERE IsDeleted = 0 AND EmailConnectorId=@emailConnectorId";
            var result = await _context.QueryFirstOrDefaultAsync<EmailConnectorConfigViewModel>(sql);

            if (result != null)
            {
                return new EmailConnector(result.EmailConnectorId, result.Name, result.EmailAddress, result.UserName, result.Password, new EmailServerConfig
                (
                    result.IMAPServer,
                    result.IMAPPort,
                    result.POP3Server,
                    result.POP3Port,
                    result.SMTPServer,
                    result.SMTPPort,
                    result.EnableSSL
                ), result.Type, result.Description);
            }

            return null;
        }
    }
}
