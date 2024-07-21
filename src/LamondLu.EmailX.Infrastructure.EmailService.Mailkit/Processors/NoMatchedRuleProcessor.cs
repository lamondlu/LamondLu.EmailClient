using System.Threading.Tasks;
using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit
{
    public class NoMatchedRuleProcessor : IRuleProcessor
    {
        public async Task Run(Email email, Rule rule)
        {

        }
    }
}