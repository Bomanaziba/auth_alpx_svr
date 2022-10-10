
using System.Collections.Generic;
using Auth.Core.Contract;

namespace Auth.Core.Responses.Client
{

    public class GetLogsResponse : ResponseBaseObject
    {
        public IList<Core.Dao.ApplicationLog> Logs { get; set; }
    
    }
    
}