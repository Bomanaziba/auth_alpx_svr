

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;

namespace Auth.Core.Repository
{
    public class SystemSettingsRepository
    {
        static string INSERT => "[dbo].[usp_SystemSettings_INSERT]";

        static string UPDATE => "[dbo].[usp_SystemSettings_UPDATE]";

        static string SELECTALL => "[dbo].[usp_SystemSettings_SELECT]";

        static string SELECTBYID => "[dbo].[usp_SystemSettings_SELECT_BY_Id]";

        static string DELETBYID => "[dbo].[usp_SystemSettings_DELETE]";

        static string SELECTBYKEY => "[dbo].[usp_SystemSettings_SELECT_BY_Key]";

        static string SELECTBYGROUP => "[dbo].[usp_SystemSettings_SELECT_BY_Group]";

        public static async Task<IList<SystemSetting>> GetSystemSettings(string conn = "")
        {
            return await Repository.ReadList<SystemSetting, string>(SELECTALL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<SystemSetting> GetSystemSetting<T>(T setting, string conn = "") where T : class
        {
            return await Repository.Read<SystemSetting, T>(setting, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<SystemSetting> GetSystemSettingByKey<T>(T setting, string conn = "") where T : class
        {
            return await Repository.Read<SystemSetting, T>(setting, SELECTBYKEY, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<IList<SystemSetting>> GetSystemSettingByGroup<T>(T setting, string conn = "") where T : class
        {
            return await Repository.ReadList<SystemSetting, T>(SELECTBYGROUP, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn), setting);
        }
        
        public static async Task<int> SaveSystemSetting<T>(T setting, string conn = "") where T : class
        {
           return await Repository.Write<int,T>(setting, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> UpdateSystemSetting<T>(T setting, string conn = "") where T : class
        {
           return await Repository.Write<int,T>(setting, UPDATE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteSystemSetting<T>(T setting, string conn = "") where T : class
        {
           return await Repository.Write<int,T>(setting, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
    }
}