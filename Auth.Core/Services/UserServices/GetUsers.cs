
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Responses;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class GetUsers : ServiceBase<GetUsersRequest, GetUsersResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetUsersResponse> PerformAction(GetUsersRequest requestObject)
        {
            if (requestObject == null)
            {
                return new GetUsersResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Request is null" }
                };
            }

            try
            {
                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString)) ? requestObject.Client.DbConnectionString : string.Empty;

                var users = await UserRepository.GetUsers(connectionString);

                return new GetUsersResponse
                {
                    Users = users,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetUsersResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };

            }

        }
    }

}