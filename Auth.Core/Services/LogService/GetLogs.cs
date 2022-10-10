
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Contract;
using Auth.Core.Dao;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Requests.User;
using Auth.Core.Responses;
using Auth.Core.Responses.Client;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class GetLogs : ServiceBase<RequestBaseObject, GetLogsResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetLogsResponse> PerformAction(RequestBaseObject obj)
        {

            try
            {
                return new GetLogsResponse
                {
                    Logs = await LogRepository.GetLogs(null),
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetLogsResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }

        }
    }

}