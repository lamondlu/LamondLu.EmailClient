using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ForwardRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public ForwardRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Run(Email email)
        {
            throw new System.NotImplementedException();
        }
    }
}