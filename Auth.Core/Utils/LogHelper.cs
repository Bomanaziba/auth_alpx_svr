

using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Auth.Core.Dao;
using Auth.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Utils
{
    public class LogHelper
    {
        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private static IHttpContextAccessor context = new HttpContextAccessor();
        
        private static string GetQueryStringData()
        {
            try
            {
                var data = new StringBuilder();

                foreach(string key in context.HttpContext.Request.Query.Keys)
                {
                    data.Append(key);
                    data.Append("=");
                    data.Append(context.HttpContext.Request.Query[key]);
                    data.Append("<br />");
                    return data.ToString();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error while retrieving query string data: {0}", e.Message);
            }

            return "";
        }

        private static string GetFormData()
        {
            try
            {
                var data = new StringBuilder();
                foreach(string key in context.HttpContext.Request.Form.Keys.Cast<string>().Where(key => String.Compare(key, "__VIEWSTATE", StringComparison.OrdinalIgnoreCase) != 0 &&
                                                                                                        String.Compare(key, "__EVENTVALIDATION", StringComparison.OrdinalIgnoreCase) != 0))
                {
                    data.Append(key);
                    data.Append("=");
                    data.Append(context.HttpContext.Request.Form[key]);
                    data.Append("<br />");
                    return data.ToString();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error while retrieve form data: {0}", e.Message);
            }
            return "";
        }

        static void Log(Exception ex, out long exceptionId, long parentId, string extraInfo, Guid chainId)
        {
            var isHttp = true;

            exceptionId = 0;

            for(; ex != null; ex = ex.InnerException)
            {
                ApplicationLog appLog = new ApplicationLog();
            
                if(context.HttpContext.User != null)
                {
                    if(context.HttpContext.User  != null && context.HttpContext.User.Identity.IsAuthenticated)
                    {
                        appLog.UserName = context.HttpContext.User.Identity.Name;
                    }
                    else
                    {
                        appLog.UserName = "[Anonymous User]";
                    }   
                }
                else
                {
                    appLog.UserName = Environment.UserName;
                    isHttp = false;
                }

                appLog.ParentId = parentId;
                appLog.MachineName = isHttp ? Dns.GetHostEntry(context.HttpContext.Connection.RemoteIpAddress).HostName : Environment.MachineName;

                appLog.MachineName += string.Format(" {0}", NetworkHelper.GetMachineIpFormatted());

                try
                {
                    appLog.UserAgent = isHttp ? context.HttpContext.Request.Headers["User-Agent"].ToString() : Environment.OSVersion.ToString();
                    appLog.Source = isHttp 
                                        ? Dns.GetHostEntry(context.HttpContext.Connection.RemoteIpAddress).HostName : Environment.MachineName ;
                                    
                }
                catch(Exception e)
                {
                    appLog.UserAgent = Environment.OSVersion.ToString();
                    appLog.Source = Environment.CurrentDirectory;
                }

                appLog.LogDate = DateTime.Now;
                appLog.LogType = ex.GetType().ToString();
                appLog.LogMessage = ex.Message;
                appLog.StackTrace = ex.StackTrace;
                appLog.QueryStringData = isHttp ? GetQueryStringData() : "";
                appLog.FormData = isHttp ? GetFormData() : "";
                appLog.ChainId = chainId;
                appLog.ExtraInfo = extraInfo;

                int id = LogRepository.SaveLog(appLog, null).Result;

                exceptionId = id;

                parentId = id;
                
            }

        }

        public static long LogException(Exception e, string extraInfo = "")
        {
            var appDao = new ApplicationLog();
            long exceptionId = 0;
            var chainId = Guid.NewGuid();
            long parentId = 0;

            try
            {
                Log(e, out exceptionId, parentId, extraInfo, chainId);

                if(e is ReflectionTypeLoadException)
                {
                    var refException = e as ReflectionTypeLoadException;

                    parentId = exceptionId;
                    
                    foreach(var loaderException in refException.LoaderExceptions)
                    {
                        Log(loaderException, out exceptionId, parentId, extraInfo, chainId);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format("Error while logginf exception: {0}: {1} - original exception: {2}: {3}, ", ex.Message, ex.StackTrace, e.Message, e.StackTrace), ex);
            }
            return exceptionId;
        }
    
    }
    
}