

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Requests.User;
using Auth.Core.Responses;
using Auth.Core.Responses.Client;
using Auth.Core.Responses.ClientResource;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class GetClientResource : ServiceBase<GetUserRequest, GetClientResourceResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetClientResourceResponse> PerformAction(GetUserRequest requestObject)
        {

            try
            {
                if (requestObject == null && requestObject.Id <= 0)
                {
                    return new GetClientResourceResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Request is null or Id cannot be less than 0" }
                    };
                }

                var res = await ClientResourceRepository.GetResouceByClientId(new { ClientId = requestObject.Id }, null);

                return new GetClientResourceResponse
                {
                    Resources = res,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetClientResourceResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }
}