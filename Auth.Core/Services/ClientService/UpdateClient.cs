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
using Auth.Core.Requests.Client;
using Auth.Core.Responses.User;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{
    public class UpdateClient : ServiceBase<UpdateClientRequest, UpdateUserResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<UpdateUserResponse> PerformAction(UpdateClientRequest requestObject)
        {
            if (requestObject == null && requestObject.Id <= 0)
            {
                return new UpdateUserResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Request is null or Id cannot be less than 0" }
                };
            }

            try
            {

                await ClientRepository.UpdateClient(new  
                {
                    Id = requestObject.Id,
                    Name = requestObject.Name,
                    Description = requestObject.Description,
                    DbConnectionString = requestObject.DbConnectionString,
                    DbType = requestObject.DbType,
                    BaseUrl = requestObject.BaseUrl,
                    IsEnabled = requestObject.IsEnabled,
                    DateModified = DateTime.Now
                }, null);

                return new UpdateUserResponse
                {
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new UpdateUserResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }
}