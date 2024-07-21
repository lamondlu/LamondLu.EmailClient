using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ClassifyRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public ClassifyRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Run(Email email, Rule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}