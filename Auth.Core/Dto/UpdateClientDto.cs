

using System;

namespace Auth.Core.Dto
{
    public class UpdateClientDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string DbConnectionString { get; set; }

        public string DbType { get; set; }

        public DateTime DateModified { get; set; }

        public bool IsEnabled { get; set; }
    }
}