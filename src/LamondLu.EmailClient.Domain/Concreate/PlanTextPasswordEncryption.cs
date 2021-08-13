using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Domain.Concreate
{
    public class PlanTextPasswordEncryption : IPasswordEncryption
    {
        public string Encrypt(string password)
        {
            return password;
        }
    }
}
