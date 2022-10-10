
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Requests.Client;
using Auth.Core.Response.Client;
using Auth.Core.Responses;
using Auth.Core.Security;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Services.UserService
{

    public class SubcribeService : ServiceBase<AddClientRequest, AddClientResponse>
    {
        private readonly ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected override async Task<AddClientResponse> PerformAction(AddClientRequest requestObject)
        {

            if (requestObject.UserId <= 0)
            {
                User user = SessionStateManager.GetUserSession();

                if (user == null)
                {
                    return new AddClientResponse
                    {
                        ResponseCode = SystemCodes.SessionExpired,
                        HttpStatusCode = HttpStatusCode.GatewayTimeout,
                        Errors = new List<string> { "Session expired" }
                    };
                }

                requestObject.UserId = user.Id;
            }

            if (requestObject == null)
            {
                return new AddClientResponse
                {
                    ResponseCode = SystemCodes.UnExpectedError,
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "Request is null or empty" }
                };
            }

            try
            {

                var id = await ClientRepository.SaveClient(new
                {
                    Id = 0,
                    Name = requestObject.Name,
                    Description = requestObject.Description,
                    DbConnectionString = requestObject.DbConnectionString,
                    DbType = requestObject.DbType,
                    IsEnabled = false,
                    DateCreated = DateTime.Now,
                    IsActive = true
                }, null);

                if (id <= 0)
                {
                    return new AddClientResponse
                    {
                        ResponseCode = SystemCodes.UnExpectedError,
                        HttpStatusCode = HttpStatusCode.ExpectationFailed,
                        Errors = new List<string> { "Client was not saved" }
                    };
                }

                string clientId = RandomGenerator.GenerateCleintId(id);
                string clientSecret = RandomGenerator.GenerateCleintSecret();

                Tuple<string, string> securedata = Password.HashPassword(clientSecret);

                _ = await ClientRepository.SaveClientCredentials(new
                {
                    Id = id,
                    ClientId = clientId,
                    ClientSecret = securedata.Item1,
                    Salt = securedata.Item2
                }, null);


                _ = await UserClientRepository.Save(new { Id = 0, UserId = requestObject.UserId, ClientId =  id});

                return new AddClientResponse
                {
                    ClientName = requestObject.Name,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    ResponseCode = SystemCodes.Successful,
                    HttpStatusCode = HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return new AddClientResponse
                {
                    ResponseCode = SystemCodes.BadRequest,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { $"An Exception Occurred: ex {ex.Message}" }
                };
            }

        }
    }

}