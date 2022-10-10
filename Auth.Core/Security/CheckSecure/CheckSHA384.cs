using System.Security.Cryptography;

namespace  Auth.Core.Security.CheckSecure
{

    public class CheckSHA384 : ICheckHashSecuredData
    {
        public bool IsHashSecureDataEqual(string data, string secureData, string salt)
        {
            SecureHashObj secure = HashAlgor.HashSecuredDataKnownSalt(data, salt, SHA384.Create());

            return secure.SecureHash == secureData;
        }
    }

}