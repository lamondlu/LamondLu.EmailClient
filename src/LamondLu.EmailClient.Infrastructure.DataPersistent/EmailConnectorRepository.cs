using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
    }
}
