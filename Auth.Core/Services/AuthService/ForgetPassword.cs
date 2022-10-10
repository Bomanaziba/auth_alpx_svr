

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Factory;
using Auth.Core.Factory;
using Auth.Core.Repository;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.Auth;
using Auth.Core.Security;
using Auth.Core.Utils;
using DbUp;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class ForgetPassword : ServiceBase<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        
        protected override async Task<ForgetPasswordResponse> PerformAction(ForgetPasswordRequest requestObject)
        {
            try
            {
                if (string.IsNullOrEmpty(requestObject.Username))
                {
                    return new ForgetPasswordResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty;

                _logger.LogInformation($"Connection string, {connectionString}"); 

                var user = await UserRepository.GetUserByUsernameOrEmail(new { Username = requestObject.Username }, connectionString);
               
                _logger.LogInformation($"Check if User: {requestObject.Username} exist");

                if(user == null)
                {
                    return new ForgetPasswordResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        ResponseDescription = "User doesn't exist.", 
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }

                var code  = Guid.NewGuid().ToString();

                await RedisManger.SetRedis($"{user.Username}::{code}", new { Id = 0, Username = user.Username, Email = user.Email, Code = code, TimeElasped = DateTime.UtcNow.AddHours(Constant.LifeSpan), DateCreated = DateTime.UtcNow }, TimeSpan.FromDays(3));

                EmailFactory.ResetPassword(requestObject.Client, code: code, fullName: $"{user.FirstName} {user.LastName}", email: user.Email, userName: user.Username);

                return new ForgetPasswordResponse
                {
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new ForgetPasswordResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    }
}