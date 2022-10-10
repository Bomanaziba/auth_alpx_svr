
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Batch;
using Auth.Core.Batch.Subscription;
using Auth.Core.Common;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests.Sub;
using Auth.Core.Response.Sub;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.Sub
{

    public class Subcribe : ServiceBase<SubRequest, SubResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<SubResponse> PerformAction(SubRequest requestObject)
        {


            try
            {
                if (string.IsNullOrEmpty(requestObject.ClientId))
                {
                    return new SubResponse
                    {
                        ResponseCode = SystemCodes.BadRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "ClientId is null or empty" }
                    };
                }

                var client = (requestObject.Client == null) ? await ClientRepository.GetClientByClientId(new { Id = requestObject.Client.Id }, null) : requestObject.Client;//(requestObject !=  null && requestObject.Client != null && !string.IsNullOrEmpty(requestObject.Client.DbConnectionString))? requestObject.Client.DbConnectionString : string.Empty ;

                if (client == null)
                {
                    return new SubResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "Client does not exist" }
                    };
                }

                var connectionString = client.DbConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    return new SubResponse
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "You need to add a connection string on you client." }
                    };
                }

                if (requestObject == null)
                {
                    return new SubResponse
                    {
                        ResponseCode = SystemCodes.InvalidRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Request is null" }
                    };
                }

                if (requestObject.SubList == null && !requestObject.SubList.Any() || requestObject.SubList.Count() <= 0)
                {
                    return new SubResponse
                    {
                        ResponseCode = SystemCodes.InvalidRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "No subscription list." }
                    };
                }
                
                List<int> subList = requestObject.SubList;

                var batchContext = new BatchContext();
                batchContext.Batches = new List<BaseBatch>();
                string jwtTokenKey = string.Empty;

                foreach (var item in subList)
                {
                    await ClientResourceRepository.Save(new { ClientId = client.Id, ResourceId = item }, null);

                    //TODO: More Resource available will be added
                    switch (item)
                    {
                        case (int)Method.Login:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "Login"));
                            jwtTokenKey = RandomGenerator.RandomAlphaNumericString(32);
                            ClientJwtKeyRepository.Save(new { ClientId = client.Id, JwtTokenKey = jwtTokenKey });
                            break;
                        case (int)Method.Register or (int)Method.Verify:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "Register"));
                            break;
                        case (int)Method.ForgetPassword:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "ForgetPassword"));
                            break;
                        case (int)Method.ResetPassword:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "ResetPassword"));
                            break;
                        default:
                            break;
                    }
                }

                await batchContext.ExecuteBatch();

                if (batchContext.Result.HttpStatusCode != HttpStatusCode.OK)
                {
                    return new SubResponse
                    {
                        ResponseCode = batchContext.Result.ResponseCode,
                        ResponseDescription = batchContext.Result.ResponseDescription,
                        HttpStatusCode = batchContext.Result.HttpStatusCode
                    };
                }

                return new SubResponse
                {
                    JwtTokenKey = jwtTokenKey,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new SubResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }

}