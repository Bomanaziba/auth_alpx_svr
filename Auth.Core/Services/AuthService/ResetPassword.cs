

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.Auth;
using Auth.Core.Security;
using Auth.Core.Utils;
using DbUp;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class ResetPassword : ServiceBase<ResetPasswordRequest, ResetPasswordResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<ResetPasswordResponse> PerformAction(ResetPasswordRequest requestObject)
        {
            try
            {
                if (string.IsNullOrEmpty(requestObject.Username))
                {
                    return new ResetPasswordResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        ResponseDescription = "Bad Credentials", 
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty;

                _logger.LogInformation($"Connection string, {connectionString}"); 

                var code = await RedisManger.GetRedis<object>($"{requestObject.Username}::{requestObject.Code}");

                if(code == null)
                {
                    return new ResetPasswordResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        ResponseDescription = "Invalid link.", 
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                var user = await UserRepository.GetUserByUsernameOrEmail(new GetUserByUsernameDto { Username = requestObject.Username }, connectionString);

                _logger.LogInformation($"Check if User: {requestObject.Username} exist");

                if(user == null)
                {
                    return new ResetPasswordResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        ResponseDescription = "User doesn't exist.", 
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }

                _logger.LogInformation($"Update user password, {user.Username}");

                Tuple<string, string> secure = Password.HashPassword(requestObject.Password);

                UserRepository.UpdateUserPasswordById(new { Id = user.Id, Password = secure.Item1, Salt = secure.Item2 }, connectionString);

                return new ResetPasswordResponse
                {
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                LogHelper.LogException(ex);

                return new ResetPasswordResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }
}