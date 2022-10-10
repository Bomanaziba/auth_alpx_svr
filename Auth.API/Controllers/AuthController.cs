using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.Auth;
using Auth.Core.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : CoreController
    {

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            var request = await ValidateRequest(user);

            var response = await ServiceRouter.Login(request); 

            return await CreateResponse<AuthResponse>(GetResponseCode(response), response);
        
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest user)
        {
            var request = await ValidateRequest(user);

            var response = await ServiceRouter.Register(request); 

            return await CreateResponse<AddUserResponse>(GetResponseCode(response), response);
        }

        [HttpGet]
        [Route("verify/{username}/{verifycode}")]
        public async Task<IActionResult> Verify(string username, string verifycode)
        {
            var request = await ValidateRequest(new VerifyAccountRequest
            {
                Username = username,
                VerifyString = verifycode
            });

            var response = await ServiceRouter.Verify(request); 

            return await CreateResponse<VerifyAccountResponse>(GetResponseCode(response), response);
        }

        [HttpPost("forget")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPassword)
        {
            var request = await ValidateRequest(forgetPassword);

            var response = await ServiceRouter.ForgotPassword(request); 

            return await CreateResponse<ForgetPasswordResponse>(GetResponseCode(response), response);
        }

        [HttpPost]
        [Route("resetpassword/{user}/{code}")]
        public async Task<IActionResult> ResetPassword(string user, string code, [FromBody]ResetPasswordRequest resetPasswordRequest)
        {
            resetPasswordRequest.Username = user;
            resetPasswordRequest.Code = code;
            var request = await ValidateRequest(resetPasswordRequest);

            var response = await ServiceRouter.ResetPassword(request); 

            return await CreateResponse<ResetPasswordResponse>(GetResponseCode(response), response);
        
        }

        [Authorize]
        [HttpGet("authorize")]
        public async Task<IActionResult> Authorize(OAuth2Request user)
        {
             var request = await ValidateRequest(user);

            var response = await ServiceRouter.Authorized(request); 

            return await CreateResponse<OAuth2Response>(GetResponseCode(response), response);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm]OAuth2Request user)
        {
            var request = await ValidateRequest(user);

            var response = await ServiceRouter.Authenticate(request); 

            return await CreateResponse<AuthResponse>(GetResponseCode(response), response);
        }
    }
    
}