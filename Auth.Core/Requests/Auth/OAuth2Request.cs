

using Auth.Core.Contract;

namespace Auth.Core.Requests.Auth
{
    public class OAuth2Request : RequestBaseObject
    {
        public string ClientSecret { get; set; }
        public string Code { get; set; }
        public string GrantType { get; set; }
        public string RedirectUri { get; set; }
        public string State { get; set; }
        public string ResponseType { get; set; }
    }
}