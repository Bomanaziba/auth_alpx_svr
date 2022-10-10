

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;

namespace Auth.Core.Repository
{
    public class ClientResourceRepository
    {
        
        private static string INSERT => "[dbo].[usp_ClientResource_INSERT]";

        static string SELECT_BY_CLIENTID_RESOURCEID => "[dbo].[usp_ClientResourceDetails_SELECT_BY_ClientId_ResourceId]";

        private static string SELECT_BY_CLIENTID => "[dbo].[usp_ClientResource_SELECT_BY_ClientId]";

        //TODO Create script
        private static string SELECT_BY_CLIENTID_RESOURCES => "[dbo].[usp_Resources_SELECT_BY_ClientId]";

        private static string SELECT_BY_RESOURCEID_CLIENTID => "[dbo].[usp_ClientResource_SELECT_BY_ClientId_ResourceId]";

        private static string DELETE_BY_ID => "[dbo].[usp_ClientResource_DELETE]";

        public static async Task<IList<ClientResource>> GetResouceByClientId<T>(T get, string conn) where T : class
        {
            return await Repository.ReadList<ClientResource, T>(SELECT_BY_CLIENTID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn), get);
        }

         public static async Task<IList<Auth.Core.Dao.Resource>> GetClientsByResource<T>(T get, string conn) where T : class
        {
            return await Repository.ReadList<Auth.Core.Dao.Resource, T>(SELECT_BY_CLIENTID_RESOURCES, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn), get);
        }
        
        public static async Task<int> Save<T>(T client, string conn) where T : class
        {
           return await Repository.Write<int, T>(client, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<ClientResource> GetClientByClientIdResourceId<T>(T get, string conn) where T : class
        {
            return await Repository.Read<ClientResource, T>(get, SELECT_BY_CLIENTID_RESOURCEID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> Delete<T>(T client, string conn) where T : class
        {
            return await Repository.Write<int, T>(client, DELETE_BY_ID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteByClientResourceId<T>(T client, string conn) where T : class
        {
            return await Repository.Write<int, T>(client, DELETE_BY_ID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}