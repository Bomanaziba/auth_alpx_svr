using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Auth.Core.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Auth.Core.Common
{
    public class CoreController : ControllerBase
    {
        private string GetHeaderValue(string header)
        {
            string val = null;

            var headerValue = new StringValues();
            if (Request.Headers.TryGetValue(header, out headerValue))
            {
                var headers = (IEnumerable<string>)headerValue;
                val = headers.FirstOrDefault();
            }

            return val;
        }

        private string ClientIdHeaderValue
        {
            get { return GetHeaderValue("ClientId"); }
        }
        
        private async Task<T> GetRequest<T>() where T : class
        {
            T s;

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var rqt = await reader.ReadToEndAsync();
                s = rqt as T;
            }
            var request = s;

            if (request is RequestBaseObject)
            {
                var baseObj = request as RequestBaseObject;

                baseObj.ClientId = ClientIdHeaderValue;
            }

            return request;
        }

        private async Task<T> GetRequest<T>(T request) where T : class
        {
            if (request == null)
            {
                request = await GetRequest<T>();
                if (request == null)
                {
                    throw new System.Exception();
                }
            }

            if (request is RequestBaseObject)
            {
                (request as RequestBaseObject).ClientId = ClientIdHeaderValue;
            }

            return request;
        }
        
        protected async Task<T> ValidateRequest<T>(T request) where T : class
        {
            request = await GetRequest<T>(request);

            return request;
        }

        protected HttpStatusCode GetResponseCode(ResponseBaseObject response)
        {
            if(response == null) return HttpStatusCode.BadRequest;

            var clientCode = ClientIdHeaderValue;

            response.ResponseDescription = SystemResponseCode.ResponseCodeDescription(response.ResponseCode, clientCode);

            return response.HttpStatusCode;
        }
        
        
        protected async Task<ObjectResult> CreateResponse<T>(HttpStatusCode statusCode, T responseObject)
        {
            return new CustomObjectResult(statusCode, responseObject);
        }
    } 
}