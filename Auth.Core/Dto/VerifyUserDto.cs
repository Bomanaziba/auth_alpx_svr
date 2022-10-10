

using System;

namespace Auth.Core.Dto
{

    public class VerifyUserDto
    {
        public string Username { get; set; }
        public string VerifyString { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateVerified { get; set; }
        public DateTime DateModified { get; set; }
    }
    
}