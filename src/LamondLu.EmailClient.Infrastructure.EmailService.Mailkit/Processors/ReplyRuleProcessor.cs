using LamondLu.EmailClient.Domain;
using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Infrastructure.EmailService.Mailkit
{
    public class ReplyRuleProcessor : IRuleProcessor
    {
private IUnitOfWork _unitOfWork = null;

        public ReplyRuleProcessor(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public void Run(Email email)
        {
            throw new System.NotImplementedException();
        }
    }
}