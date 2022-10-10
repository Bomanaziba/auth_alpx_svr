

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Dto;

namespace Auth.Core.Repository
{

    public class UserRepository
    {
        static string INSERT => "[dbo].[usp_User_INSERT]";

        static string REGISTER => "[dbo].[usp_User_REGISTER]";

        static string UPDATE => "[dbo].[usp_User_UPDATE]";

        static string SELECTBYID => "[dbo].[usp_User_SELECT_BY_Id]";

        static string SELECTBYUSERNAME => "[dbo].[usp_User_SELECT_BY_Username]";

        static string SELECTBYUSERNAMEEMAIL => "[dbo].[usp_User_SELECT_BY_Username_and_Email]";

        static string UPDATEUSERPASSWORDBYID => "[dbo].[usp_User_UPDATE_PASSWORD_BY_ID]";

        static string SELECTBYEMAIL => "[dbo].[usp_User_SELECT_BY_Email]";

        static string SELECTALL => "[dbo].[usp_User_SELECT]";

        static string DELETBYID => "[dbo].[usp_User_DELETE]";

        private static string DEACTIVATE => "[dbo].[usp_User_DEACTIVE]";

        private static string VERIFYUSER => "[dbo].[usp_User_VERIFY]";

        public static async Task<IList<User>> GetUsers(string conn)
        {
            return await Repository.ReadList<User, string>(SELECTALL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<User> GetUser<T>(T get, string conn) where T : class
        {
            return await Repository.Read<User, T>(get, SELECTBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }
        
        public static async Task<int> SaveUser<T>(T user, string conn) where T : class
        {
           return await Repository.Write<int,T>(user, INSERT, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<long> RegisterUser<T>(T user, string conn) where T : class
        {
           return await Repository.Write<int,T>(user, REGISTER, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> UpdateUser<T>(T update, string conn) where T : class
        {
            return await Repository.Write<int, T>(update, UPDATE, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> DeleteUser<T>(T delete, string conn) where T : class
        {
            return await Repository.Write<int, T>(delete, DELETBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<User> GetUserByUsername<T>(T get, string conn) where T : class
        {
            return await Repository.Read<User, T>(get, SELECTBYUSERNAME, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<User> GetUserByEmail<T>(T get, string conn) where T : class
        {
            return await Repository.Read<User, T>(get, SELECTBYEMAIL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> VerifiedUser<T>(T update, string conn) where T : class
        {
            return await Repository.Read<int, T>(update, VERIFYUSER, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<User> GetUserByUsernameOrEmail<T>(T get, string conn) where T : class
        {
            return await Repository.Read<User, T>(get, SELECTBYUSERNAMEEMAIL, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

        public static async Task<int> UpdateUserPasswordById<T>(T update, string conn) where T : class
        {
            return await Repository.Read<int, T>(update, UPDATEUSERPASSWORDBYID, CommandType.StoredProcedure, AppSetting.GetConnectionString(conn));
        }

    }
    
}