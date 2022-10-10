

using Auth.Core.Contract;

namespace Auth.Core.Requests.Auth
{
    public class LoginRequest : RequestBaseObject
    {

        public string Password { get; set; }

        public string Username { get; set; }
        
    }

    public class RegisterRequest : RequestBaseObject
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
        
    }
}