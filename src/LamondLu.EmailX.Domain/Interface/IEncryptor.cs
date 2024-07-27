namespace LamondLu.EmailX.Domain.Interface
{
    public interface IEncrypt
    {
        string Encrypt(string text);

        string Decrypt(string text);
    }
}