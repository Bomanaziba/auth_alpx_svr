

using System;
using Auth.Core.Contract;

namespace Auth.Core.Requests.User
{

    public class AddUserRequest :  RequestBaseObject
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int Nationality { get; set; }

        public int Race { get; set; }

    }
    
}