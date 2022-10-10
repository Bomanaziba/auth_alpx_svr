

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class UserVerificationCodeRepository
    {   
        static string INSERT => "[dbo].[usp_Code_INSERT]";

        static string SELECTBYID => "[dbo].[usp_Code_SELECT_BY_USERNAME]";

        static string usp_Code_SELECT_BY_USERNAME_CODE => "[dbo].[usp_Code_SELECT_BY_USERNAME_CODE]";

        static string usp_Code_DELETE_BY_USERNAME_CODE => "[dbo].[usp_Code_DELECT_BY_USERNAME_CODE]";

        static string DELETBYID => "[dbo].[usp_Code_DELETE_BY_USERNAME]";


        public static async Task<UserVerificationCode> GetCodeByUserName<T>(T get,string conn)
        {
            return await Repository.Read<UserVerificationCode, T>(get, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<UserVerificationCode> GetCodeByUserNameCode<T>(T get,string conn)
        {
            return await Repository.Read<UserVerificationCode, T>(get, usp_Code_SELECT_BY_USERNAME_CODE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<int> SaveCode<T>(T code, string conn) where T : class
        {
           return await Repository.Write<int, T>(code, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteCodeByUserName<T>(T code, string conn) where T : class
        {
            return await Repository.Write<int, T>(code, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteCodeByUserNameCode<T>(T code, string conn) where T : class
        {
            return await Repository.Write<int, T>(code, usp_Code_DELETE_BY_USERNAME_CODE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}