

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class LogRepository
    {   
        static string INSERT => "[dbo].[usp_Log_INSERT]";

        static string SELECTBYID => "[dbo].[usp_Log_SELECT_BY_Id]";

        static string SELECTALL => "[dbo].[usp_Log_SELECT]";

        static string DELETBYID => "[dbo].[usp_Log_DELETE]";


        public static async Task<IList<ApplicationLog>> GetLogs(string conn)
        {
            return await Repository.ReadList<ApplicationLog, string>(SELECTALL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<ApplicationLog> GetLog<T>(T get, string conn) where T : class
        {
            return await Repository.Read<ApplicationLog, T>(get, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<int> SaveLog<T>(T client, string conn) where T : class
        {
           return await Repository.Write<int, T>(client, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteLog<T>(T client, string conn) where T : class
        {
            return await Repository.Write<int, T>(client, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}