

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
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class GetClient : ServiceBase<GetUserRequest, GetClientResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetClientResponse> PerformAction(GetUserRequest requestObject)
        {

            try
            {
                if (requestObject == null && requestObject.Id <= 0)
                {
                    return new GetClientResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Request is null or Id cannot be less than 0" }
                    };
                }

                return new GetClientResponse
                {
                    Client = await ClientRepository.GetClient(new { Id = requestObject.Id }, null),
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetClientResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }
}