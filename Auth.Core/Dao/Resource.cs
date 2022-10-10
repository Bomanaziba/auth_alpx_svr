

using System;

namespace Auth.Core.Dao
{

    public class Resource
    {
        public long Id { get; set; }

        public long ResourceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long ServiceId { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
    
}