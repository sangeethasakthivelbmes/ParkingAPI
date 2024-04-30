using System.Security.Cryptography;
using System.Text;

namespace Parking.WebAPI.CoreHelper.CryptoHelper
{
    public static class ActivityLogger
    {
        public static string EncryptPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPassword = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}

