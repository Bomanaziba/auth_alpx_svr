

using System.Threading.Tasks;
using Auth.Core.Dao;
using Auth.Core.Requests.User;
using Auth.Core.Requests.Auth;
using Auth.Core.Responses.Auth;
using Auth.Core.Services.AuthService;
using Auth.Core.Services.UserService;
using Auth.Core.Responses.User;
using Auth.Core.Requests.Client;
using Auth.Core.Response.Client;
using Auth.Core.Responses.Client;
using Auth.Core.Services;
using Auth.Core.Response.Sub;
using Auth.Core.Requests.Sub;
using Auth.Core.Contract;
using Auth.Core.Requests;
using Auth.Core.Responses.ClientResource;
using Auth.Core.Responses;
using Auth.Core.Services.Sub;

namespace Auth.Core.Common
{
    public class ServiceRouter
    {

        #region AuthService Region

        public async static Task<AddUserResponse> Register(RegisterRequest user)
        {
            var parameter = new ServiceParameter<RegisterRequest, AddUserResponse, Register>(user, Method.Register);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<AuthResponse> Login(LoginRequest user)
        {
            var parameter = new ServiceParameter<LoginRequest, AuthResponse, Login>(user, Method.Login);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<VerifyAccountResponse> Verify(VerifyAccountRequest user)
        {
            var parameter = new ServiceParameter<VerifyAccountRequest, VerifyAccountResponse, VerifyAccount>(user, Method.Verify);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<ForgetPasswordResponse> ForgotPassword(ForgetPasswordRequest request)
        {
            var parameter = new ServiceParameter<ForgetPasswordRequest, ForgetPasswordResponse, ForgetPassword>(request, Method.ForgetPassword);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var parameter = new ServiceParameter<ResetPasswordRequest, ResetPasswordResponse, ResetPassword>(request, Method.ResetPassword);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<AuthResponse> Authenticate(OAuth2Request request)
        {
            var parameter = new ServiceParameter<OAuth2Request, AuthResponse, Authenticate>(request, Method.Authenticate);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<OAuth2Response> Authorized(OAuth2Request request)
        {
            var parameter = new ServiceParameter<OAuth2Request, OAuth2Response, Authorize>(request, Method.Authorized);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        #endregion



        #region UserService Region

        public async static Task<AddUserResponse> AddUser(AddUserRequest usersRequest)
        {
            var parameter = new ServiceParameter<AddUserRequest, AddUserResponse, CreateUser>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetUsersResponse> GetUsers(GetUsersRequest usersRequest)
        {
            var parameter = new ServiceParameter<GetUsersRequest, GetUsersResponse, GetUsers>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetUserResponse> GetUser(GetUserRequest usersRequest)
        {
            var parameter = new ServiceParameter<GetUserRequest, GetUserResponse, GetUser>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<UpdateUserResponse> UpdateUser(UpdateUserRequest usersRequest)
        {
            var parameter = new ServiceParameter<UpdateUserRequest, UpdateUserResponse, UpdateUser>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<DeleteUserResponse> DeleteUser(DeleteUserRequest usersRequest)
        {
            var parameter = new ServiceParameter<DeleteUserRequest, DeleteUserResponse, DeleteUser>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }
        #endregion

        #region Roles

        #endregion

        #region Client Region

        public async static Task<AddClientResponse> AddClient(AddClientRequest client)
        {
            var parameter = new ServiceParameter<AddClientRequest, AddClientResponse, SubcribeService>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetClientsResponse> GetClients(GetUsersRequest client)
        {
            var parameter = new ServiceParameter<GetUsersRequest, GetClientsResponse, GetClients>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetClientResponse> GetClient(GetUserRequest client)
        {
            var parameter = new ServiceParameter<GetUserRequest, GetClientResponse, GetClient>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetClientsResponse> GetUserClients(GetUsersRequest client)
        {
            var parameter = new ServiceParameter<GetUsersRequest, GetClientsResponse, GetClients>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<UpdateUserResponse> UpdateClient(UpdateClientRequest usersRequest)
        {
            var parameter = new ServiceParameter<UpdateClientRequest, UpdateUserResponse, UpdateClient>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<DeleteUserResponse> DeleteClient(DeleteUserRequest usersRequest)
        {
            var parameter = new ServiceParameter<DeleteUserRequest, DeleteUserResponse, DeleteClient>(usersRequest);

            return await ServiceCall.ExecuteMethod(parameter);
        }

         public async static Task<GetClientResourceResponse> GetResource(GetUserRequest client)
        {
            var parameter = new ServiceParameter<GetUserRequest, GetClientResourceResponse, GetClientResource>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        #endregion


        #region Subscription

        public async static Task<SubResponse> Subscribe(SubRequest res)
        {
            var parameter = new ServiceParameter<SubRequest, SubResponse, Subcribe>(res);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<ResponseBaseObject> UnSubscribe(SubRequest res)
        {
            var parameter = new ServiceParameter<SubRequest, ResponseBaseObject, UnSubscribe>(res);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<SubscriptionsResponse> Subscriptions(GetSubRequest client)
        {
            var parameter = new ServiceParameter<GetSubRequest, SubscriptionsResponse, Subscriptions>(client);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        #endregion

        #region  Application Log

        public async static Task<GetLogsResponse> GetApplicationLogs()
        {
            var parameter = new ServiceParameter<RequestBaseObject, GetLogsResponse, GetLogs>();

            return await ServiceCall.ExecuteMethod(parameter);
        }

        public async static Task<GetLogResponse> GetApplicationLog(GetLogRequest res)
        {
            var parameter = new ServiceParameter<GetLogRequest, GetLogResponse, GetLog>(res);

            return await ServiceCall.ExecuteMethod(parameter);
        }

        #endregion
    }
}