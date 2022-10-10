
using Auth.Core.Contract;

namespace Auth.Core.Requests.User
{
    public class GetUserRequest : RequestBaseObject
    {
        public int Id { get; set; }
    }
}