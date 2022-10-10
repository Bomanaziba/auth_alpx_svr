

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Factory;
using Auth.Core.Repository;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.User;
using Auth.Core.Security;
using Auth.Core.Utils;
using DbUp;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class Register : ServiceBase<RegisterRequest, AddUserResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<AddUserResponse> PerformAction(RegisterRequest requestObject)
        {

            try
            {
                requestObject.Username = (string.IsNullOrEmpty(requestObject.Username)) ? requestObject.Email : requestObject.Username;

                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty;

                User user = await UserRepository.GetUserByUsernameOrEmail(new { Username = requestObject.Username }, connectionString);

                if (user != null)
                {
                    return new AddUserResponse
                    {
                        ResponseCode = SystemCodes.UserAlreayExist,
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                Tuple<string, string> secure = Password.HashPassword(requestObject.Password);

                long id = await UserRepository.RegisterUser(new
                {
                    Id = 0,
                    Username = requestObject.Username,
                    Email = requestObject.Email,
                    Password = secure.Item1,
                    Salt = secure.Item2,
                    IsEnabled = true,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }, connectionString);

                if(id <= 0) 
                {
                    return new AddUserResponse
                    {
                        ResponseCode = SystemCodes.UnExpectedError,
                        HttpStatusCode = HttpStatusCode.ServiceUnavailable
                    };
                }

                user = await UserRepository.GetUser(new { Id = id }, connectionString);

                SendAccountVerifcation(requestObject.Client, user);

                return new AddUserResponse
                {
                    ResponseCode = SystemCodes.Successful, 
                    HttpStatusCode = HttpStatusCode.Created,
                    ResponseDescription = "Complete registration by verifying account from email."
                    
                };
            }
            catch (Exception ex)
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
    
        private async Task SendAccountVerifcation(Client client, User user)
        {
            var verifyString = RandomGenerator.RandomAlphaString(AppSetting.AccountVerificationCodeLength);

            await RedisManger.SetRedis(verifyString, new {
                UserName = user.Username,
                Email = user.Email,
                Code = verifyString,}, TimeSpan.FromMinutes(10));

            await EmailFactory.AccountVerification(client, email: user.Email, fullName: $"{user.FirstName} {user.LastName}",  userName: user.Username, code: verifyString);

        }
    
    }
}