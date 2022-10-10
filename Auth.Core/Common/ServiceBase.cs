

using System;
using System.Threading.Tasks;
using Auth.Core.Contract;

namespace Auth.Core.Common
{
    public abstract class ServiceBase<T, TK> where T : RequestBaseObject where TK : ResponseBaseObject
    {
        internal async Task<TK> Execute(MethodParameter<T, TK> parameter = null) 
        {
            try
            {
                var response = await Execute(parameter.RequestObject);

                return ServiceCall.ReturnResponseFormat<TK>(response);
            }
            catch(Exception e)
            {
                return ServiceCall.FormatError<TK>(e);
            }
        }

        public async Task<TK> Execute(T requestObject = null) 
        {
            try
            {
                return await PerformAction(requestObject);
            }
            catch(Exception e)
            {
                return ServiceCall.FormatError<TK>(e);
            }
        }

        protected abstract Task<TK> PerformAction(T requestObject = null);
    }
}
    
