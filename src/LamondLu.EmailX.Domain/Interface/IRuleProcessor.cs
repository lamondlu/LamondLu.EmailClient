using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Interface
{
    public interface IRuleProcessor
    {
        Task Run(Email email, Rule rule);
    }
}