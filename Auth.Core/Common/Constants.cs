

namespace Auth.Core.Common
{
    
    public class Constant
    {
        public const string AlphaX = "AX";

        public const string ServiceId = "AS";

        public const string TokenType = "bearer";

        public const string GrantType = "authorization_code";

        public const string ResponseType = "code";

        public const int LifeSpan = 24;

        public const string SessionKey = "_loggeduser";
    }

    

    public enum Method
    {
        Register = 1,
        Login = 2,
        Verify = 3,
        ForgetPassword = 4,
        ResetPassword = 5,
        ForgottenPassword = 6,
        Authenticate = 7,
        Authorized = 8
    }


    public enum EmailType
    {
        Verify,
        ResetPassword
    }
}