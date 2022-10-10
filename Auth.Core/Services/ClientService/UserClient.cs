
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

    public class UserClient : ServiceBase<GetUsersRequest, GetClientsResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetClientsResponse> PerformAction(GetUsersRequest requestObject)
        {


            try
            {
                if (requestObject == null)
                {
                    return new GetClientsResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Request is null" }
                    };
                }

                int userId = 0;

                if(requestObject.UserId <= 0) 
                {
                    User user = SessionStateManager.GetUserSession();
                    if(user != null) userId = user.Id;
                }

                 if (userId <= 0)
                {
                    return new GetClientsResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "No userId" }
                    };
                }

                return new GetClientsResponse
                {
                    Clients = await UserClientRepository.Get(new { UserId = userId }),
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetClientsResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }

        }
    }

}