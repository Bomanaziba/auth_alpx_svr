
using Auth.Core.Dao;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Web;

namespace Auth.Core.Common
{

    public class SessionStateManager
    {
        private static IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor(); 

        public static void SetUserSession(User user)
        {
            if(user == null) return;
            
            _httpContextAccessor.HttpContext.Session.Set(Constant.SessionKey, user);
        }

        public static User GetUserSession()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get(Constant.SessionKey);
            return user == null ? default : JsonSerializer.Deserialize<User>(user);
        }
    }
    
}