

using Auth.Core.Contract;

namespace Auth.Core.Requests.Client
{

    public class UpdateClientRequest : RequestBaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DbConnectionString { get; set; }
        public int DbType { get; set; }
        public bool IsEnabled { get; set; }
        public string BaseUrl { get; set; }
    }
    
}