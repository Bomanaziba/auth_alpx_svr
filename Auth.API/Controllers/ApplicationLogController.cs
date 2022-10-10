using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Requests;
using Auth.Core.Responses.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{


    [Route("api/v1/application")]
    [ApiController]
    [Authorize]
    public class ApplicationLogController : CoreController
    {
        
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {

            var response = await ServiceRouter.GetApplicationLogs(); 

            return await CreateResponse<GetLogsResponse>(GetResponseCode(response), response);
        
        }

         [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]GetLogRequest res)
        {
            var request = await ValidateRequest(res);

            var response = await ServiceRouter.GetApplicationLog(request); 

            return await CreateResponse<GetLogResponse>(GetResponseCode(response), response);
     
        }

    }
}