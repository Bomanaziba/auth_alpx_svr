

using Auth.Core.Contract;

namespace Auth.Core.Requests.Client
{

    public class AddClientRequest : RequestBaseObject
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DbConnectionString { get; set; }
        public int DbType { get; set; }
    }
    
}