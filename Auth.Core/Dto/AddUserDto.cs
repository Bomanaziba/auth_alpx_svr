
using System;

namespace Auth.Core.Dto
{
    public class AddUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Nationality { get; set; }
        public int Race { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateCreated { get; set; }
    }
    
}