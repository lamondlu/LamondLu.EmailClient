using System;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ReplyRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public ReplyRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Run(Email email)
        {
            Console.WriteLine($"System send out reply email 'RE: {email.Subject}'");
        }
    }
}