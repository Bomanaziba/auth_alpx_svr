using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Contract;

namespace Auth.Core.Batch.Subscription
{
    public class BaseEngine : BaseBatch
    {

        private readonly string _connectionString;
        private readonly string _sqlFolder;

        public BaseEngine(string conn, string sqlFolder)
        {
            _connectionString = conn;
            _sqlFolder = sqlFolder;
        }

        public async Task<ResponseBaseObject> ExecuteBatch()
        {
            try
            {
                var res = MigrationEngine.RunScript(_connectionString, _sqlFolder);

                if (res.Successful == false)
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = SystemCodes.UnExpectedError,
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseBaseObject
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { e.Message }
                };
            }

            return new ResponseBaseObject
            {
                ResponseCode = SystemCodes.Successful,
                HttpStatusCode = HttpStatusCode.OK

            };
        }
    }

}


