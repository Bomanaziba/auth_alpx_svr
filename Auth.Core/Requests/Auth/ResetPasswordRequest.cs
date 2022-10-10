


using Auth.Core.Contract;

namespace Auth.Core.Requests.Auth
{

    public class ResetPasswordRequest : RequestBaseObject
    {
        public string Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}