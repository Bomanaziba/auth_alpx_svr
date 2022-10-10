using System;

namespace Auth.Core.Dto
{

    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? GenderId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? NationalityId { get; set; }

        public int? RaceId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public bool? IsEnabled { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastSeen { get; set; }

        public DateTime DateModified { get; set; }
    }
    
}