using Auth.Core.Contract;

namespace Auth.Core.Responses.User
{
    public class GetUserResponse : ResponseBaseObject
    {
        public Core.Dao.User User { get; set; }
    }
}