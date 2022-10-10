

using System.Collections.Generic;
using System.Net;

namespace Auth.Core.Contract
{

    public class ResponseBaseObject
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        
        public List<string> Errors { get; set; }
        
        public string ResponseCode  { get; set; }

        public string ResponseDescription { get; set; }
    }
    
}