using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace apiInmobiliariaLabIII
{
    public static class HashHelper
    {
        public static string Hashear(string clave, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: clave,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
        }
    }
}
