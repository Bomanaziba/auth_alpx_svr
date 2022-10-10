


using System;

namespace Auth.Core.Dao
{
    public class ClientJwtTokenKey
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string JwtTokenKey { get; set; }
        public DateTime DateCreated { get; set; }
    }
}