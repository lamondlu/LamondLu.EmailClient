namespace LamondLu.EmailClient.Domain.Interface
{
    public interface IPasswordEncryption
    {
        string Encrypt(string password);
    }
}
