

using System.Collections.Generic;
using Auth.Core.Contract;

namespace Auth.Core.Responses.ClientResource
{
    public class GetClientResourceResponse : ResponseBaseObject
    {
        public IList<Core.Dao.ClientResource> Resources { get; set; }
    }
    
}