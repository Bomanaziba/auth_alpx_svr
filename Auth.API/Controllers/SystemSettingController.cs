using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Repository;
using Auth.Core.Requests;
using Auth.Core.Response;
using Auth.Core.Responses.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{


    [Route("api/v1/systemsetting")]
    [ApiController]
    [Authorize]
    public class SystemSettingController : CoreController
    {
        
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                SystemSettingsResponse response = new SystemSettingsResponse
                {
                    Settings = await SystemSettingsRepository.GetSystemSettings(null),
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<SystemSettingsResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
               
         }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                SystemSettingResponse response = new SystemSettingResponse
                {
                    Setting = await SystemSettingsRepository.GetSystemSetting(new { Id = id }, null),
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<SystemSettingResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("key/{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            try
            {
                SystemSettingResponse response = new SystemSettingResponse
                {
                    Setting = await SystemSettingsRepository.GetSystemSettingByKey(new { Key = key }, null),
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<SystemSettingResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("group/{group}")]
        public async Task<IActionResult> GetByGroup(string group)
        {
            try
            {
                SystemSettingsResponse response = new SystemSettingsResponse
                {
                    Settings = await SystemSettingsRepository.GetSystemSettingByGroup(new { Group = group }, null),
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<SystemSettingsResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]AddSystemSettingRequest res)
        {
            try
            {
                await SystemSettingsRepository.SaveSystemSetting(new
                {
                    Key = res.Key,
                    Value = res.Value,
                    Group = res.Group,
                    Description = res.Description,
                    Id = 0
                }, null);

                AddSystemSettingResponse response = new AddSystemSettingResponse
                {
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<AddSystemSettingResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]AddSystemSettingRequest res)
        {
            try
            {
                await SystemSettingsRepository.UpdateSystemSetting(new 
                {
                    Key = res.Key,
                    Value = res.Value,
	                Group = res.Group,
	                Description = res.Description,
	                Id = res.Id
                }, null);

                AddSystemSettingResponse response = new AddSystemSettingResponse
                {
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };

                return await CreateResponse<AddSystemSettingResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await SystemSettingsRepository.DeleteSystemSetting(new { Id = id }, null);

                AddSystemSettingResponse response = new AddSystemSettingResponse
                {
                    ResponseCode = "00",
                    HttpStatusCode = System.Net.HttpStatusCode.OK
                };


                return await CreateResponse<AddSystemSettingResponse>(GetResponseCode(response), response);
            }
            catch(Exception ex)
            {
                throw ex;
            }   
        }



    }
}