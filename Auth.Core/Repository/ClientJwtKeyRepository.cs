

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class ClientJwtKeyRepository
    {   
        static string INSERT => "[dbo].[usp_ClientJwtTokenKey_INSERT]";

        static string UPDATE => "[dbo].[usp_ClientJwtTokenKey_UPDATE]";

        static string SELECTBYID => "[dbo].[usp_ClientJwtTokenKey_SELECT_BY_ID]";

        static string SELECTALL => "[dbo].[usp_ClientJwtTokenKey_SELECT]";

        static string DELETBYID => "[dbo].[usp_ClientJwtTokenKey_DELETE]";


        public static async Task<IList<ClientJwtTokenKey>> All(string conn = "")
        {
            return await Repository.ReadList<ClientJwtTokenKey, string>(SELECTALL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<ClientJwtTokenKey> Get<T>(T get, string conn = "") where T : class
        {
            return await Repository.Read<ClientJwtTokenKey, T>(get, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<int> Save<T>(T client, string conn = "") where T : class
        {
           return await Repository.Write<int, T>(client, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> Update<T>(T client, string conn = "") where T : class
        {
            return await Repository.Write<int, T>(client, UPDATE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> Delete<T>(T client, string conn = "") where T : class
        {
            return await Repository.Write<int, T>(client, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}