using System.Security.Cryptography;

namespace  Auth.Core.Security.CheckSecure
{

    public class CheckSHA256 : ICheckHashSecuredData
    {
        public bool IsHashSecureDataEqual(string data, string secureData, string salt)
        {
            SecureHashObj secure = HashAlgor.HashSecuredDataKnownSalt(data, salt, SHA256.Create());

            return secure.SecureHash == secureData;
        }
    }

}