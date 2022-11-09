using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MessagerMVC.Services
{
    public static class HashService
    {
        public static string GetHash(string password)
        {
            byte[] salt = new byte[128 / 8];

            for (int i = 0; i < salt.Length; i++)
                salt[i] = Convert.ToByte('r');

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}