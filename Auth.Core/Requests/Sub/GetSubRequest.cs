using Auth.Core.Contract;

namespace Auth.Core.Requests
{

    public class  GetSubRequest : RequestBaseObject
    {
        public long ClientId { get; set; }
    }
    
}