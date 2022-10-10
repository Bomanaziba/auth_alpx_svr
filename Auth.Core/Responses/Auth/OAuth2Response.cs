

using Auth.Core.Contract;

namespace Auth.Core.Responses.Auth
{

    public class OAuth2Response : ResponseBaseObject
    {
        public string RedirectUri { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
    }
    
}