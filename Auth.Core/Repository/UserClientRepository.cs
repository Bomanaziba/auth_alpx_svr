

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class UserClientRepository
    {   
        static string INSERT => "[dbo].[usp_UserClient_INSERT]";

        static string SELECTBYUSERID => "[dbo].[usp_UserClient_SELECT_BY_USERID]";


        public static async Task<IList<Client>> Get<T>(T get, string conn = "")
        {
            return await Repository.ReadList<Client, T>(SELECTBYUSERID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn), get);
        }

        public static async Task<int> Save<T>(T user, string conn = "") where T : class
        {
           return await Repository.Write<int,T>(user, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }


    }
    
}