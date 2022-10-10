

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Auth.Core.Batch;
using Auth.Core.Contract;

namespace Auth.Core.Common
{

    public class BatchContext
    {
        public List<BaseBatch> Batches { get; set; }

        public ResponseBaseObject Result { get; set; }

        public async Task<ResponseBaseObject> ExecuteBatch()
        {
            foreach(var batch in Batches)
            {
                try
                {
                    Result = await batch.ExecuteBatch();
                }
                catch(Exception e)
                {
                    return new ResponseBaseObject
                    {
                        ResponseCode = SystemCodes.UnExpectedError,
                        ResponseDescription = batch.GetType().ToString()+e.Message,
                        HttpStatusCode = HttpStatusCode.InternalServerError
                    };
                }

            }

            return Result;
        }
    }
    
}