

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Core.Dao;
using Auth.Core.Repository;

namespace Auth.Core.Services
{
    public class SystemSettingService : CacheService
    {

        static string gKey = BuildCacheKey<SystemSettingService>();

        public static async Task AddSystemSetting(SystemSetting systemSetting)
        {
            try
            {
                await SystemSettingsRepository.SaveSystemSetting(
                new
                {
                    Key = systemSetting.Key,
                    Value = systemSetting.Value,
                    Description = systemSetting.Description,
                    Group = systemSetting.Group
                }, null
            );

                Remove($"{gKey}::{systemSetting.Key}");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static async Task UpdateSystemSetting(SystemSetting systemSetting)
        {
            try
            {
                await SystemSettingsRepository.UpdateSystemSetting(
                                new
                                {
                                    Key = systemSetting.Key,
                                    Value = systemSetting.Value,
                                    Description = systemSetting.Description,
                                    Group = systemSetting.Group
                                }, null
                            );

                Remove($"{gKey}::{systemSetting.Key}");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static async Task DeleteSystemSetting(SystemSetting systemSetting)
        {
            try
            {
                await SystemSettingsRepository.DeleteSystemSetting(
                new
                {
                    Id = systemSetting.Id
                }, null
            );

                Remove($"{gKey}::{systemSetting.Key}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<SystemSetting> GetSystemSettingByKey(string key)
        {

            try
            {
                SystemSetting data = Get<SystemSetting>($"{gKey}::{key}");

                if (data == null)
                {
                    data = await SystemSettingsRepository.GetSystemSettingByKey(new { Key = key });

                    Add<SystemSetting>($"{gKey}::{key}", data);
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static async Task<IList<SystemSetting>> GetSystemSettingByGroup(string group)
        {

            try
            {
                IList<SystemSetting> data = Get<List<SystemSetting>>($"{gKey}::{group}");

                if (data == null)
                {
                    data = await SystemSettingsRepository.GetSystemSettingByGroup(new { Group = group });
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}