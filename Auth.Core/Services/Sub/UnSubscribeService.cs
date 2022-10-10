



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Batch;
using Auth.Core.Batch.Subscription;
using Auth.Core.Common;
using Auth.Core.Contract;
using Auth.Core.Repository;
using Auth.Core.Requests.Sub;
using Auth.Core.Response.Sub;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services
{
    public class UnSubscribe : ServiceBase<SubRequest, ResponseBaseObject>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<ResponseBaseObject> PerformAction(SubRequest requestObject)
        {
            try
            {
                if (requestObject.Client == null)
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "Client does not exist" }
                    };
                }

                var client = requestObject.Client;

                var connectionString = client.DbConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = SystemCodes.NotFound,
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "You need to add a connection string on you client." }
                    };
                }

                if (requestObject.SubList == null && !requestObject.SubList.Any() || requestObject.SubList.Count() <= 0)
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = SystemCodes.InvalidRequest,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "No subscription list requested." }
                    };
                }

                List<int> subList = requestObject.SubList;

                var batchContext = new BatchContext();
                batchContext.Batches = new List<BaseBatch>();

                foreach (var item in subList)
                {
                    await ClientResourceRepository.Save(new { ClientId = client.Id, ResourceId = item }, null);

                    switch (item)
                    {
                        case (int)Method.Login:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "LoginDrop"));
                            break;
                        case (int)Method.Register or (int)Method.Verify:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "RegisterDrop"));
                            break;
                        case (int)Method.ForgetPassword:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "ForgetPasswordDrop"));
                            break;
                        case (int)Method.ResetPassword:
                            batchContext.Batches.Add(new BaseEngine(connectionString, "ResetPasswordDrop"));
                            break;
                        default:
                            break;
                    }
                }

                await batchContext.ExecuteBatch();

                if (batchContext.Result.HttpStatusCode != HttpStatusCode.OK)
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = batchContext.Result.ResponseCode,
                        ResponseDescription = batchContext.Result.ResponseDescription,
                        HttpStatusCode = batchContext.Result.HttpStatusCode
                    };
                }

                return new ResponseBaseObject
                {
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