


using System;

namespace Auth.Core.Dto
{
    public class RegisterDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
        
        public bool IsActive { get; set; }

        public bool IsEnabled{ get; set; }

        public DateTime DateCreated { get; set; }

    }
    
}