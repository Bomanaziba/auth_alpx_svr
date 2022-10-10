using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Requests;
using Auth.Core.Requests.User;
using Auth.Core.Responses;
using Auth.Core.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    [Route("api/v1/user")]
    [ApiController]
    [Authorize]
    public class UserController : CoreController
    {
        
        [HttpGet("list")]
        public async Task<IActionResult> List([FromHeader]GetUsersRequest userRequest)
        {
            var request = await ValidateRequest(userRequest);

            var response  = await ServiceRouter.GetUsers(userRequest);

            return await CreateResponse<GetUsersResponse>(GetResponseCode(response), response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]GetUserRequest userRequest)
        {
            var request = await ValidateRequest(userRequest);

            var response  = await ServiceRouter.GetUser(userRequest);

            return await CreateResponse<GetUserResponse>(GetResponseCode(response), response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]AddUserRequest user)
        {
            var request = await ValidateRequest(user);

            var response  = await ServiceRouter.AddUser(request);

            return await CreateResponse<AddUserResponse>(GetResponseCode(response), response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]UpdateUserRequest userRequest)
        {
            var request = await ValidateRequest(userRequest);

            var response  = await ServiceRouter.UpdateUser(userRequest);

            return await CreateResponse<UpdateUserResponse>(GetResponseCode(response), response);
        }


        [HttpPost("delete")]
        public async Task<IActionResult> delete([FromQuery]DeleteUserRequest userRequest)
        {
            var request = await ValidateRequest(userRequest);

            var response  = await ServiceRouter.DeleteUser(userRequest);

            return await CreateResponse<DeleteUserResponse>(GetResponseCode(response), response);
        }
    }
}