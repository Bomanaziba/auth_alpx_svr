

using System;

namespace Auth.Core.Dao
{

    public class UserVerificationCode
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Code { get; set; }

        public DateTime TimeElasped { get; set; }

        public DateTime DateCreated { get; set; }
    }
    
}