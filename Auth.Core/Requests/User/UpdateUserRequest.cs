using System;
using Auth.Core.Contract;

namespace Auth.Core.Requests.User
{
    public class UpdateUserRequest : RequestBaseObject
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? GenderId { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Nationality { get; set; }

        public int Race { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastSeen { get; set; }
    }
}