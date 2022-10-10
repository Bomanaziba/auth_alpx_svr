using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Repository;
using Auth.Core.Requests.User;
using Auth.Core.Responses.User;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class DeleteClient : ServiceBase<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<DeleteUserResponse> PerformAction(DeleteUserRequest requestObject)
        {

            if (requestObject == null && requestObject.Id <= 0)
            {
                return new DeleteUserResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Request is null or Id cannot be less than 0" }
                };
            }

            try
            {
                var id = await ClientRepository.DeleteClient(new { Id = requestObject.Id }, null);
 
                if (id <= 0)
                {
                    return new DeleteUserResponse
                    {
                        ResponseCode = SystemCodes.InvalidRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Client not found" }
                    };
                }

                return new DeleteUserResponse
                {
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new DeleteUserResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }

}