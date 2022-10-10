


using Auth.Core.Contract;

namespace Auth.Core.Requests.Auth
{

    public class VerifyAccountRequest : RequestBaseObject
    {
        public string VerifyString { get; set; }
        public string Username { get; set; }
    }
    
}