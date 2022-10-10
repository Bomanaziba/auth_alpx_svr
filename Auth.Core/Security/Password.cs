

using System;
using Auth.Core.Dao;
using Auth.Core.Security.CheckSecure;

namespace Auth.Core.Security
{

    public class Password
    {
        public static Tuple<string, string> HashPassword(string password)
        {
            SecureHashObj obj = HashAlgor.HashSecuredData(password, HashConstant.SaltLength, HashConstant.SecureSHA512);

            return new Tuple<string, string>(obj.SecureHash, obj.Salt);
        }

        public static bool PasswordCheck(string password, string storePassword, string salt)
        {
            ICheckHashSecuredData  isEqual = new CheckSHA512();
            return isEqual.IsHashSecureDataEqual(password, storePassword, salt);
        }
    }
    
}