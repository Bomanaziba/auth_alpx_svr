

using System;

namespace Auth.Core.Dao
{

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Salt { get; set; }  
        public string DbConnectionString { get; set; }
        public int DbType { get; set; }
        public string BaseUrl { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
    
}