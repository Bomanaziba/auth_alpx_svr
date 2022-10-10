using System.Collections.Generic;
using Auth.Core.Common;
using Auth.Core.Contract;

namespace Auth.Core.Responses
{
    public class SubscriptionsResponse : ResponseBaseObject
    {
        public Core.Dao.Client Client { get; set; }

        public IList<Core.Dao.Resource> Resources { get; set; }
    }
}