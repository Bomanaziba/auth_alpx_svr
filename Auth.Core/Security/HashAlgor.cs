
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Core.Security
{
    public class HashAlgor
    {
        public static string HashData(string data, HashAlgorithm hashAlgorithm)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] resultHash = hashAlgorithm.ComputeHash(dataBytes);
            return Convert.ToBase64String(dataBytes);
        }

        public static SecureHashObj HashSecuredData(string data, int saltLength, HashAlgorithm hashAlgorithm)
        {
            byte[] saltBytes = GenerateRandomCryptographicBytes(saltLength);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            List<byte> dataWithSalt = new List<byte>();
            dataWithSalt.AddRange(dataBytes);
            dataWithSalt.AddRange(saltBytes);
            byte[] resultHash = hashAlgorithm.ComputeHash(dataWithSalt.ToArray());
            return new SecureHashObj(Convert.ToBase64String(saltBytes), Convert.ToBase64String(dataBytes), Convert.ToBase64String(resultHash));
        }

        public static SecureHashObj HashSecuredDataKnownSalt(string data, string salt, HashAlgorithm hashAlgorithm)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            List<byte> dataWithSalt = new List<byte>();
            dataWithSalt.AddRange(dataBytes);
            dataWithSalt.AddRange(saltBytes);
            byte[] resultHash = hashAlgorithm.ComputeHash(dataWithSalt.ToArray());
            return new SecureHashObj(Convert.ToBase64String(saltBytes), Convert.ToBase64String(dataBytes), Convert.ToBase64String(resultHash));
        }

        private static string GenerateRandomCryptographicKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }
        
        private static byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            RNGCryptoServiceProvider  rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rNGCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }
    }


    
    
}