
using Auth.Core.Contract;

namespace Auth.Core.Requests
{
    public class GetUsersRequest : RequestBaseObject
    {
        public int UserId { get; set; }
    }
}