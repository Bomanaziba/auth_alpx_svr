

using Auth.Core.Contract;

namespace Auth.Core.Responses.Auth
{

    public class AuthResponse : ResponseBaseObject
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }

    }
    
}