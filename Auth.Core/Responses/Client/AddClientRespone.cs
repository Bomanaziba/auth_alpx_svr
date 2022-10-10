


using Auth.Core.Contract;

namespace Auth.Core.Response.Client
{

    public class AddClientResponse : ResponseBaseObject
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    
}