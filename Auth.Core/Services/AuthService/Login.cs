

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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class Login : ServiceBase<LoginRequest, AuthResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<AuthResponse> PerformAction(LoginRequest requestObject)
        {
            try
            {

                if (string.IsNullOrEmpty(requestObject.Username) || string.IsNullOrEmpty(requestObject.Password))
                {
                    return new AuthResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }

                var res = await Authenticate(requestObject);

                if (!res.Item2)
                {
                    return new AuthResponse
                    {
                        ResponseCode = SystemCodes.Unauthorized,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.Unauthorized
                    };
                }

                string tokenKey = string.Empty;

                if (requestObject.Client != null)
                {
                    var token = await ClientJwtKeyRepository.Get(new { ClientId = requestObject.Client.Id });

                    tokenKey = token.JwtTokenKey;
                }
                else
                {
                    tokenKey = AppSetting.JwtToken;
                }

                var access_token = GenerateToken.GenerateAccessToken(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.PrimarySid, res.Item1.Id.ToString()),
                        new Claim(ClaimTypes.Name, res.Item1.Username),
                        new Claim("FirstName", res.Item1.FirstName),
                        new Claim("LastName", res.Item1.LastName),
                        new Claim(ClaimTypes.Email, res.Item1.Email),
                        new Claim(ClaimTypes.DateOfBirth, res.Item1.DateOfBirth.ToString("dddd, dd MMMM yyyy")),
                        new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(3).Ticks.ToString())
                    }
                , tokenKey);

                var refresh_token = GenerateToken.GenerateRefreshToken();

                return new AuthResponse
                {
                    Id = res.Item1.Id,
                    Username = res.Item1.Username,
                    FirstName = res.Item1.FirstName,
                    LastName = res.Item1.LastName,
                    Email = res.Item1.Email,
                    AccessToken = access_token,
                    RefreshToken = refresh_token,
                    TokenType = Constant.TokenType,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new AuthResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }

        }

        private async Task<Tuple<User, bool>> Authenticate(LoginRequest requestObject)
        {
            try
            {
                _logger.LogInformation($"Check for null, {requestObject}");

                if (string.IsNullOrEmpty(requestObject.Username) || string.IsNullOrEmpty(requestObject.Password)) return new Tuple<User, bool>(null, false);

                var connectionString = (requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString)) ? requestObject.Client.DbConnectionString : string.Empty;

                _logger.LogInformation($"Connection string, {connectionString}");

                var user = await UserRepository.GetUserByUsernameOrEmail(new { Username = requestObject.Username }, connectionString);

                if (user == null) return new Tuple<User, bool>(null, false);

                _logger.LogInformation($"User details, {user.Username}");

                bool isValid = Password.PasswordCheck(requestObject.Password, user.Password, user.Salt);

                if (!isValid) return new Tuple<User, bool>(null, isValid);

                SessionStateManager.SetUserSession(user);

                return new Tuple<User, bool>(user, isValid);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}