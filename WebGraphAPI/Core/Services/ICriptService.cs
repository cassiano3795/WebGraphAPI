using System.Security.Cryptography;

namespace Core.Services
{
    public interface ICriptService
    {
        string Encrypt(string password);
        bool VerifyPassword(MD5 md5Hash, string password, string hash);
    }
}
