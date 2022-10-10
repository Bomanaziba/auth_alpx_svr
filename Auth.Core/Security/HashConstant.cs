

using System.Security.Cryptography;

namespace Auth.Core.Security
{
    public class HashConstant
    {
        public static int SaltLength = 64; 
        public static HashAlgorithm SecureMD5 = MD5.Create();
        public static HashAlgorithm SecureSHA1 = SHA1.Create();
        public static HashAlgorithm SecureSHA256 = SHA256.Create();
        public static HashAlgorithm SecureSHA384 = SHA384.Create();
        public static HashAlgorithm SecureSHA512 = SHA512.Create();
    }

    
}