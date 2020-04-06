using System;
using System.Security.Cryptography;
using System.Text;
using Core.Services;

namespace Services.Services
{
    public class CriptService : ICriptService
    {
        private const string HashEncript = "ALGUMA-COISA-PRA-ENCRIPTAR??";
        public string Encrypt(string password)
        {
            var passwordHash = HashEncript + password;

            var md5Hash = MD5.Create();

            return GetMd5Hash(md5Hash, passwordHash);

        }

        public bool VerifyPassword(MD5 md5Hash, string password, string hash)
        {
            var passwordHash = HashEncript + password;

            var hashOfInput = GetMd5Hash(md5Hash, passwordHash);

            var comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var dt in data)
            {
                sBuilder.Append(dt.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
