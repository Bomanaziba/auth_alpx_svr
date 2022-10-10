using System.Collections.Generic;
using Auth.Core.Contract;

namespace Auth.Core.Responses
{
    public class GetUsersResponse : ResponseBaseObject
    {
        public IList<Core.Dao.User> Users { get; set; }
    }
}