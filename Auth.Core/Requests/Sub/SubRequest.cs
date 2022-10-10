

using System.Collections.Generic;
using Auth.Core.Contract;

namespace Auth.Core.Requests.Sub
{
    public class SubRequest : RequestBaseObject
    {   
        public List<int> SubList { get; set; }
    }
    
}