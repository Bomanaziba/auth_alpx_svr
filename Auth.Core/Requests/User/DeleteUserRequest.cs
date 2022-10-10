
using Auth.Core.Contract;

namespace Auth.Core.Requests.User
{
    public class DeleteUserRequest : RequestBaseObject
    {
        public int Id { get; set; }
    }
}