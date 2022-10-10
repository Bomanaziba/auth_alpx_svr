

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Contract;
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

    public class VerifyAccount : ServiceBase<VerifyAccountRequest, VerifyAccountResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<VerifyAccountResponse> PerformAction(VerifyAccountRequest requestObject)
        {
            try
            {
                if (string.IsNullOrEmpty(requestObject.VerifyString) || string.IsNullOrEmpty(requestObject.Username))
                {
                    return new VerifyAccountResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty;

                _logger.LogInformation($"Connection string, {connectionString}"); 

                if(!await IsCodeValid(requestObject))
                {
                    return new VerifyAccountResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        ResponseDescription = "Code Invalid",
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }
               
                await Verify(requestObject, connectionString);

                return new VerifyAccountResponse
                {
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new VerifyAccountResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }
    
        private async Task Verify(VerifyAccountRequest obj, string connectionString)
        {
            
            await UserRepository.VerifiedUser(new
            {
                Username = obj.Username,
                IsVerified = true,
                DateVerified = DateTime.UtcNow,
                DateModified = DateTime.UtcNow 

            }, connectionString);

        }

        private async Task<bool> IsCodeValid(VerifyAccountRequest obj)
        {
            var value = await RedisManger.GetRedis<object>(obj.VerifyString);

            return value == null ? false : true;
        }
    
    }
}