
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Contract;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Responses.Client;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class GetLog : ServiceBase<GetLogRequest, GetLogResponse>
    {

        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<GetLogResponse> PerformAction(GetLogRequest obj)
        {
            try
            {
                var val = await LogRepository.GetLog(new { Id = obj.RequestId }, null);

                if (val == null)
                {
                    return new GetLogResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }

                return new GetLogResponse
                {
                    Id = val.Id,
                    ParentId = val.ParentId,
                    MachineName = val.MachineName,
                    UserName = val.UserName,
                    UserAgent = val.UserAgent,
                    LogDate = val.LogDate,
                    LogType = val.LogType,
                    LogMessage = val.LogMessage,
                    Source = val.Source,
                    StackTrace = val.StackTrace,
                    QueryStringData = val.QueryStringData,
                    FormData = val.FormData,
                    ChainId = val.ChainId, 
                    ExtraInfo = val.ExtraInfo,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new GetLogResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }

        }
    }

}