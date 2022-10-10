using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Requests;
using Auth.Core.Requests.Client;
using Auth.Core.Requests.User;
using Auth.Core.Response.Client;
using Auth.Core.Responses.Client;
using Auth.Core.Responses.ClientResource;
using Auth.Core.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    [Route("api/v1/client")]
    [ApiController]
    [Authorize]
    public class ClientController : CoreController
    {
        
        [HttpGet("list")]
        public async Task<IActionResult> List([FromHeader]GetUsersRequest res)
        {
            var request = await ValidateRequest(res);

            var response = await ServiceRouter.GetClients(request); 

            return await CreateResponse<GetClientsResponse>(GetResponseCode(response), response);
        
        }

        [HttpGet("user")]
        public async Task<IActionResult> User([FromQuery]GetUsersRequest res)
        {
            var request = await ValidateRequest(res);

            var response = await ServiceRouter.GetUserClients(request); 

            return await CreateResponse<GetClientsResponse>(GetResponseCode(response), response);
        
        }


        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]GetUserRequest res)
        {
            var request = await ValidateRequest(res);

            var response = await ServiceRouter.GetClient(request); 

            return await CreateResponse<GetClientResponse>(GetResponseCode(response), response);
     
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]AddClientRequest client)
        {
            var request = await ValidateRequest(client);

            var response = await ServiceRouter.AddClient(request); 

            return await CreateResponse<AddClientResponse>(GetResponseCode(response), response);
     
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]UpdateClientRequest client)
        {
            var request = await ValidateRequest(client);

            var response = await ServiceRouter.UpdateClient(request); 

            return await CreateResponse<UpdateUserResponse>(GetResponseCode(response), response);
     
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery]DeleteUserRequest client)
        {
            var request = await ValidateRequest(client);

            var response = await ServiceRouter.DeleteClient(request); 

            return await CreateResponse<DeleteUserResponse>(GetResponseCode(response), response);
        }

        [HttpGet("resource")]
        public async Task<IActionResult> Resource([FromQuery]GetUserRequest res)
        {
            var request = await ValidateRequest(res);

            var response = await ServiceRouter.GetResource(request); 

            return await CreateResponse<GetClientResourceResponse>(GetResponseCode(response), response);
     
        }

    }
}