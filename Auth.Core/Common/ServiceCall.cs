

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Auth.Core.Contract;
using Auth.Core.Dao;
using Auth.Core.Dto;
using Auth.Core.Repository;
using Auth.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Common
{

    public class ServiceCall
    {

        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        public async static Task<TResponse> ExecuteMethod<TResquest, TResponse, TMethod>(ServiceParameter<TResquest, TResponse, TMethod> Parameter = null)
        where TResquest : RequestBaseObject
        where TResponse : ResponseBaseObject
        where TMethod : ServiceBase<TResquest, TResponse>
        {
            var parameter = Parameter.Parameter;

            try
            {

                if (parameter.RequestObject != null && parameter.RequestObject.ClientId != AppSetting.SystemClient)
                {
                    if (string.IsNullOrEmpty(parameter.RequestObject.ClientId))
                    {
                        return GetStandardResponseFormat<TResponse>(responseCode: SystemCodes.NotFound, responseDescritpion: "You did not pass clientId in the header: ClientId is null.", statusCode: HttpStatusCode.NotFound);
                    }

                    parameter.RequestObject.Client = await ClientRepository.GetClientByClientId(new GetClientDto { ClientId = parameter.RequestObject.ClientId }, null);

                    if (parameter.RequestObject.Client == null)
                    {
                        return GetStandardResponseFormat<TResponse>(responseCode: SystemCodes.NotFound, responseDescritpion: "Client does not exit", statusCode: HttpStatusCode.NotFound);
                    }

                    if (parameter.Resource > 0)
                    {
                        var availabelResource = await ClientResourceRepository.GetClientByClientIdResourceId(new { ClientId = parameter.RequestObject.Client.Id, ResourceId = (int)parameter.Resource }, null);

                        if (availabelResource == null)
                        {
                            return GetStandardResponseFormat<TResponse>(responseCode: SystemCodes.NotFound, responseDescritpion: "You have not subscribe to any resource.", statusCode: HttpStatusCode.NotFound);
                        }

                        if (availabelResource.ResourceId != (int)parameter.Resource)
                        {
                            return GetStandardResponseFormat<TResponse>(responseCode: SystemCodes.Unauthorized, responseDescritpion: "You did not have access to this resource.", statusCode: HttpStatusCode.Unauthorized);
                        }
                    }

                }

                var response = await Parameter.Method.Execute(parameter);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                LogHelper.LogException(ex);

                return FormatError<TResponse>(ex);
            }
        }

        public static T FormatError<T>(Exception e) where T : ResponseBaseObject
        {
            return FormatError<T>(e, null, null);
        }

        public static T FormatError<T>(Exception e, string extraInfo, string responseDescription) where T : ResponseBaseObject
        {
            return GetStandardResponseFormat<T>(SystemCodes.UnExpectedError, e.Message + responseDescription);
        }

        public static T GetStandardResponseFormat<T>(string responseCode, string responseDescritpion = "",
                        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) where T : ResponseBaseObject
        {
            var response = Activator.CreateInstance(typeof(T)) as T;

            if (response == null) return null;

            response.ResponseCode = responseCode;

            response.ResponseDescription = SystemResponseCode.ResponseCodeDescription(response.ResponseCode, response.ResponseDescription);
            response.Errors = new List<string> { responseDescritpion };
            response.HttpStatusCode = statusCode;

            return ReturnResponseFormat<T>(response);
        }

        public static T ReturnResponseFormat<T>(T response) where T : ResponseBaseObject
        {
            return response;
        }
    }
}

