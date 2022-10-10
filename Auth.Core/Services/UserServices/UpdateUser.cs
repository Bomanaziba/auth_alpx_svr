using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests.User;
using Auth.Core.Responses.User;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{
    public class UpdateUser : ServiceBase<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<UpdateUserResponse> PerformAction(UpdateUserRequest requestObject)
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


                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString)) ? requestObject.Client.DbConnectionString : string.Empty;

                if (await UserRepository.UpdateUser(new
                {
                    Id = requestObject.Id,
                    FirstName = requestObject.FirstName,
                    LastName = requestObject.LastName,
                    GenderId = requestObject.GenderId,
                    DateOfBirth = requestObject.DateOfBirth,
                    NationalityId = requestObject.Nationality,
                    RaceId = requestObject.Race,
                    Username = requestObject.Username,
                    Email = requestObject.Email,
                    DateModified = DateTime.Now
                }, connectionString) <= 0)
                {
                    return new UpdateUserResponse
                    {
                        ResponseCode = SystemCodes.InvalidRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "User not found" }
                    };
                }

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