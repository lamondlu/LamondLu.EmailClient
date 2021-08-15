using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.DataPersistent
{
    public class EmailRepository: IEmailRepository
    {
        private DapperDbContext _context = null;

        public EmailRepository(DapperDbContext context)
        {
            _context = context;
        }

    }
}