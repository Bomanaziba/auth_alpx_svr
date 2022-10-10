using System.Security.Cryptography;

namespace  Auth.Core.Security.CheckSecure
{

    public class CheckMD5 : ICheckHashSecuredData
    {
        public bool IsHashSecureDataEqual(string data, string secureData, string salt)
        {
            SecureHashObj secure = HashAlgor.HashSecuredDataKnownSalt(data, salt, MD5.Create());

            return secure.SecureHash == secureData; 
        }
    }

}