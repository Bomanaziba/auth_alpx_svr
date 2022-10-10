

namespace Auth.Core.Dto
{
    public class UpdateUserPasswordDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}