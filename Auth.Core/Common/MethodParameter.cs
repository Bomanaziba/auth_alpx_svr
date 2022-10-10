
using Auth.Core.Contract;

namespace Auth.Core.Common
{
    public class MethodParameter<T, TK> where T: RequestBaseObject where TK: ResponseBaseObject
    {
        public T RequestObject { get; set; }
        
        public TK ResponseObject { get; set; }

        public Method Resource { get; set; }

        public override string ToString()
        {
            return RequestObject.ToString();
        }
    }
    
}