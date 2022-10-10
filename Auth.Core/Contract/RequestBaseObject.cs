using Auth.Core.Dao;

namespace Auth.Core.Contract
{
    public class RequestBaseObject
    {
        public string ClientId { get; set; }
        public Client Client { get; set; }
    }
    
}