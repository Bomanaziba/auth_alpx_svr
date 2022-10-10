
using Auth.Core.Contract;
using Auth.Core.Dao;

namespace Auth.Core.Responses.Client
{

    public class GetClientResponse : ResponseBaseObject
    {
        public Core.Dao.Client Client { get; set; }
    }
    
}