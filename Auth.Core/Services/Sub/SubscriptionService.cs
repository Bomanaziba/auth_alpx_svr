

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Responses;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services
{

    public class Subscriptions : ServiceBase<GetSubRequest, SubscriptionsResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<SubscriptionsResponse> PerformAction(GetSubRequest requestObject)
        {
            try
            {
                if (requestObject.Client == null)
                {
                    return new SubscriptionsResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "Client does not exist" }
                    };
                }

                var connectionString = requestObject.Client.DbConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    return new SubscriptionsResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "You need to add a connection string on you client." }
                    };
                }

                IList<Auth.Core.Dao.Resource> resources = await ClientResourceRepository.GetClientsByResource(new { ClientId = requestObject.Client.ClientId }, connectionString);

                if (resources == null && !resources.Any() || resources.Count() <= 0)
                {
                    return new SubscriptionsResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NoContent,
                        Errors = new List<string> { "No subscription list requested." }
                    };
                }

                return new SubscriptionsResponse
                {
                    Client = requestObject.Client,
                    Resources = resources,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new SubscriptionsResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { ex.Message }
                };
            }


        }
        
    }
    
}