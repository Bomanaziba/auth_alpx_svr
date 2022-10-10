
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

    public class CreateUser : ServiceBase<AddUserRequest, AddUserResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);


        protected override async Task<AddUserResponse> PerformAction(AddUserRequest requestObject)
        {

            if(requestObject == null)
            {
                return new AddUserResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>{ "Request is null or empty" }
                };
            }


            try
            {

            var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty;

            var id = await UserRepository.SaveUser(new 
            {
                FirstName = requestObject.FirstName,
                LastName = requestObject.LastName,
                GenderId = requestObject.GenderId,
                DateOfBirth = requestObject.DateOfBirth,
                Nationality = requestObject.Nationality,
                Race = requestObject.Race,
                Username = requestObject.Username,
                Email = requestObject.Email,
                DateCreated = DateTime.Now,
                IsActive = true
            }, connectionString);

            if(id<=0)
            {
                return new AddUserResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.ExpectationFailed,
                    Errors = new List<string>{ "User was not saved" }
                };
            }

            return new AddUserResponse
            {
                ResponseCode = SystemCodes.Successful,
                HttpStatusCode = HttpStatusCode.Created
            };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new AddUserResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }

}