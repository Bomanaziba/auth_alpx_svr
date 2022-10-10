using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Contract;
using Auth.Core.Dao;
using Auth.Core.Requests;
using Auth.Core.Requests.Sub;
using Auth.Core.Response.Sub;
using Auth.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    
    [Route("api/v1/subscription")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : CoreController
    {

        [HttpGet("list")]
        public async Task<IActionResult> List(GetSubRequest client)
        {
            var request = await ValidateRequest(client);
            
            var response = await ServiceRouter.Subscriptions(request); 

            return await CreateResponse<SubscriptionsResponse>(GetResponseCode(response), response);
        
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubRequest sub)
        {
            var request = await ValidateRequest(sub);

            var response = await ServiceRouter.Subscribe(request); 

            return await CreateResponse<SubResponse>(GetResponseCode(response), response);
        
        }

        [HttpPost("unsubscribe")]
        public async Task<IActionResult> UnSubscribe(SubRequest sub)
        {
            var request = await ValidateRequest(sub);

            var response = await ServiceRouter.UnSubscribe(request); 

            return await CreateResponse<ResponseBaseObject>(GetResponseCode(response), response);
        
        }

    }
    
}