

using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Core.Common
{
    public class CustomObjectResult : ObjectResult
    {
        public CustomObjectResult(HttpStatusCode statusCode, object obj) : base(obj)
        {
            StatusCode = (int) statusCode;
        }

    }
    
}