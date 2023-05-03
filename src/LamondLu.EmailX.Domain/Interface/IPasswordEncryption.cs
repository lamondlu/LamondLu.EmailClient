namespace LamondLu.EmailX.Domain.Interface
{
    public interface IPasswordEncryption
    {
        string Encrypt(string password);
    }
}
