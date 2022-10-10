

using System;
using Auth.Core.Contract;

namespace Auth.Core.Common
{

    public class ServiceParameter<T, TK, TKt> 
    where T : RequestBaseObject
    where TK: ResponseBaseObject
    where TKt: ServiceBase<T, TK>
    {
        public MethodParameter<T, TK> Parameter { get; private set;}

        public Method ResourceId { get; private set;}

        public ServiceParameter(T request = null, Method resourceId = 0)
        {
            ResourceId = resourceId;
            Parameter = new MethodParameter<T, TK>
            {
                RequestObject = request,
                Resource = ResourceId
            };
        }

        public ServiceBase<T, TK> Method
        {
            get
            {
                return Activator.CreateInstance<TKt>();
            }
        }
    } 
    
}