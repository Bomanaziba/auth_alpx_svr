

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.Auth;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.AuthService
{

    public class Authorize : ServiceBase<OAuth2Request, OAuth2Response>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<OAuth2Response> PerformAction(OAuth2Request requestObject)
        {
            List<string> error = new List<string>();

            if (string.IsNullOrEmpty(requestObject.ClientId) || string.IsNullOrEmpty(requestObject.ClientSecret))
            {
                error.Add("ClientId or ClientSecret is null or empty");

                return new OAuth2Response
                {
                    RedirectUri = requestObject.RedirectUri + "?error=unsupported_response_type",
                    ResponseCode = SystemCodes.BadRequest,
                    ResponseDescription = "Bad Credentials",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = error
                };
            }

            if (requestObject.ResponseType != Constant.ResponseType)
            {
                error.Add("Unsupported response type");

                return new OAuth2Response
                {
                    ResponseCode = SystemCodes.BadRequest,
                    ResponseDescription = "Bad Credentials",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = error
                };
            }


            try
            {

                if (!await Authenticate.IsClientValid(requestObject))
                {
                    error.Add("Access denied");

                    return new OAuth2Response
                    {
                        RedirectUri = requestObject.RedirectUri + "?error=access_denied",
                        ResponseCode = SystemCodes.Unauthorized,
                        ResponseDescription = "Bad Credentials",
                        HttpStatusCode = HttpStatusCode.Unauthorized
                    };
                }


                string authCode = Guid.NewGuid().ToString(); //Get to redis. When set up
                string user = ""; //Save to redis. when set up

                //TODO: Save the above to redis when done.

                return new OAuth2Response
                {
                    RedirectUri = requestObject.RedirectUri,
                    Code = authCode,
                    State = requestObject.State,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new OAuth2Response
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }
        }

    }

}