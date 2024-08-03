using System;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class ClassifyRuleProcessor : IRuleProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassifyRuleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Run(Email email, Rule rule)
        {
            Console.WriteLine($"System try to add tag '{(rule as ClassifyRule).Tag}' to email '{email.Subject}'");
            try
            {
                await _unitOfWork.EmailTagRepository.AddTagToEmail(email.EmailId, (rule as ClassifyRule).Tag);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add tag '{(rule as ClassifyRule).Tag}' to email '{email.Subject}', error: {ex.Message}");
            }
        }
    }
}