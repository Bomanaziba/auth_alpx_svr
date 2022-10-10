


using Auth.Core.Contract;

namespace Auth.Core.Requests.Auth
{

    public class ForgetPasswordRequest : RequestBaseObject
    {
        public string Username { get; set; }
    }
    
}