

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class ClientRepository
    {   
        static string INSERT => "[dbo].[usp_Client_INSERT]";

        static string INSERT_CLIENT_CREDENTIALS => "[dbo].[usp_Client_INSERT_Credentials]";

        static string UPDATE => "[dbo].[usp_Client_UPDATE]";

        static string SELECTBYID => "[dbo].[usp_Client_SELECT_BY_Id]";

        static string SELECT_BY_CLIENTID => "[dbo].[usp_Client_SELECT_BY_ClientId]";

        static string SELECTBYNAME => "[dbo].[usp_Client_SELECT_BY_Name]";

        static string SELECTALL => "[dbo].[usp_Client_SELECT]";

        static string DELETBYID => "[dbo].[usp_Client_DELETE]";

        static string DEACTIVATE => "[dbo].[usp_Client_DEACTIVE]";


        public static async Task<IList<Client>> GetClients(string conn)
        {
            return await Repository.ReadList<Client, string>(SELECTALL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<Client> GetClient<T>(T get, string conn) where T : class
        {
            return await Repository.Read<Client, T>(get, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<Client> GetClientByClientId<T>(T get, string conn) where T : class
        {
            return await Repository.Read<Client, T>(get, SELECT_BY_CLIENTID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<Client> GetClientByName<T>(T get, string conn) where T : class
        {
            return await Repository.Read<Client, T>(get, SELECTBYNAME, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<int> SaveClient<T>(T client, string conn) where T : class
        {
           return await Repository.Write<int, T>(client, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> SaveClientCredentials<T>(T client, string conn) where T : class
        {
           return await Repository.Write<int, T>(client, INSERT_CLIENT_CREDENTIALS, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> UpdateClient<T>(T client, string conn) where T : class
        {
            return await Repository.Write<int, T>(client, UPDATE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteClient<T>(T client, string conn) where T : class
        {
            return await Repository.Write<int, T>(client, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}