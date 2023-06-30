namespace API.Interfaces
{
    public interface IEncryptor
    {
        string EncryptPassword(string password);
    }
}