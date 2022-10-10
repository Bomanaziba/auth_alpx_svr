
using System.Collections.Generic;
using Auth.Core.Contract;

namespace Auth.Core.Responses.Client
{

    public class GetClientsResponse : ResponseBaseObject
    {
        public IList<Core.Dao.Client> Clients { get; set; }
    
    }
    
}