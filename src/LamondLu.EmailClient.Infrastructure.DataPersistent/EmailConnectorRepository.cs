using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LamondLu.EmailClient.Domain;

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
                emailConnector.EmailAddress,
                emailConnector.Password,
                emailConnector.Status,
                IP = emailConnector.Server.Server,
                emailConnector.Server.Port,
                emailConnector.Server.EnableSSL,
                emailConnector.Description,
                emailConnector.Type
            });
        }
    }
}
