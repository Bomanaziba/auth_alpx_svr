
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
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class Authenticate : ServiceBase<OAuth2Request, AuthResponse>
    {
        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<AuthResponse> PerformAction(OAuth2Request requestObject)
        {
            List<string> error = new List<string>();

            if (string.IsNullOrEmpty(requestObject.ClientId) || string.IsNullOrEmpty(requestObject.ClientSecret))
            {
                error.Add("ClientId or ClientSecret is null or empty");

                return new AuthResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    ResponseDescription = "Bad Credentials",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = error
                };
            }

            if (requestObject.GrantType != Constant.GrantType)
            {
                error.Add("Unsupported grant type");

                return new AuthResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    ResponseDescription = "Bad Credentials",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = error
                };
            }

            if (requestObject.GrantType != Constant.GrantType)
            {
                error.Add("Unsupported grant type");

                return new AuthResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    ResponseDescription = "Bad Credentials",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = error
                };
            }

            try
            {

                if (!await IsClientValid(requestObject))
                {
                    error.Add("Access denied");

                    return new AuthResponse
                    {
                        ResponseCode = SystemCodes.Unauthorized,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.Unauthorized
                    };
                }

                string previousUri = ""; //Get from redis. When set up
                string user = ""; //Get from redis. when set up

                //TODO: Delete the above from redis when done.

                if (requestObject.RedirectUri != previousUri)
                {
                    error.Add("RedirectUri was inconsistent.");

                    return new AuthResponse
                    {
                        ResponseCode = SystemCodes.UnExpectedError,
                        ResponseDescription = "Conflict of result",
                        HttpStatusCode = HttpStatusCode.Conflict,
                        Errors = error
                    };
                }

                if (string.IsNullOrEmpty(user))
                {
                    error.Add("User associated with the given code not found.");

                    return new AuthResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        ResponseDescription = "User not Found",
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = error
                    };
                }

                var access_token = GenerateToken.GenerateAccessToken(
                    new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user)
                    }
                );

                var refresh_token = GenerateToken.GenerateRefreshToken();

                return new AuthResponse
                {
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

        public static async Task<bool> IsClientValid(OAuth2Request requestObject)
        {
            try
            {
                if (string.IsNullOrEmpty(requestObject.ClientId) || string.IsNullOrEmpty(requestObject.ClientSecret)) return false;

                var client = await ClientRepository.GetClient(new GetClientDto { ClientId = requestObject.ClientId }, null);

                if (client == null) return false;

                bool isPasswordSame = Password.PasswordCheck(requestObject.ClientSecret, client.ClientSecret, client.Salt);

                if (isPasswordSame) return isPasswordSame;

                return isPasswordSame;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

    }

}