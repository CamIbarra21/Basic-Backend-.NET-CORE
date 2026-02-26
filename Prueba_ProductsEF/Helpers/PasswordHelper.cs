using System.Security.Cryptography;

namespace Prueba_ProductsEF.Helpers
{
    public class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int iterations = 10000;

        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;
        public static string HashPassword(string password)
        {

            //Generar salt
            byte[] saltByte = RandomNumberGenerator.GetBytes(SaltSize);

            //Derivar la contraseña utilizando PBKDF2
            byte[] hashByte = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, iterations, algorithm, HashSize);

            return $"{Convert.ToHexString(hashByte)}:{Convert.ToHexString(saltByte)}";
        }

        public static bool VerifyPassword(string password, string storedValue)
        {
            //storedValue tiene formato "hash:salt"
            var parts = storedValue.Split(':');
            if (parts.Length != 2) return false;

            string storedHash = parts[0];
            string storedSalt = parts[1];

            byte[] saltBytes = Convert.FromHexString(storedSalt);

            //Recalcular hash con la contraseña ingresada y el mismo salt
            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, iterations, algorithm, HashSize);
            string computedHash = Convert.ToHexString(hashBytes);

            return computedHash == storedHash;
        }
    }
}
