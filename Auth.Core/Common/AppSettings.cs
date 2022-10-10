using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Repository;
using Auth.Core.Services;
using Auth.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Serialization;

namespace Auth.Core.Common
{
    public class AppSetting
    {
        private static T GetAppSettings<T>(string key, string group = default(string), T defaultValue = default(T), bool throwExceptionIfNotFound = false)
        {
            try
            {
                IConfigurationRoot _configurableRoot = new ConfigurationBuilder()
            .SetBasePath($@"{Paths.ApplicationBasePath}")
            .AddJsonFile("appsettings.json")
            .Build();

                T setting = default(T);

                if (!string.IsNullOrWhiteSpace(group))
                {
                    setting = _configurableRoot.GetValue<T>($"{group}:{key}");
                }
                else
                {
                    setting = _configurableRoot.GetValue<T>(key);
                }

                if (setting == null && defaultValue != null)
                {
                    setting = defaultValue;
                }

                if (setting == null)
                {
                    setting = Task.Run(() => GetTAsync<T>(key, group)).Result;
                }

                if (throwExceptionIfNotFound && EqualityComparer<T>.Default.Equals(setting, default(T)))
                {
                    throw new Exception($"{group}:{key} not found in configurations");
                }

                return setting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<T> GetTAsync<T>(string key, string group = "")
        {
            try
            {
                if (string.IsNullOrEmpty(key)) return default(T);

                var result = await SystemSettingService.GetSystemSettingByKey(key);

                if (result == null) return default(T);

                if (result.Value.StartsWith("{") && result.Value.EndsWith("}")) return JsonSerializer.Deserialize<T>(result.Value);

                if (result.Value.StartsWith("<") && result.Value.EndsWith(">"))
                {
                    using (Stream reader = new FileStream(result.Value, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(reader);
                    }
                }

                 var res = (object)result.Value;

                 return (T)res;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static string SystemEmail => GetAppSettings<string>("SystemEmail");

        public static string SystemAdmin => GetAppSettings<string>("SystemAdmin");

        public static string EmailAPIKey => GetAppSettings<string>("EmailAPIKey");

        public static string JwtToken => GetAppSettings<string>("JwtToken");

        public static int AccountVerificationDuration => GetAppSettings<int>("AccountVerificationDuration");

        public static int AccountVerificationCodeLength => GetAppSettings<int>("AccountVerificationCodeLength");

        public static int ShortCodeVerificationDuration => GetAppSettings<int>("ShortCodeVerificationDuration");

        public static int ShortCodeVerificationCodeLength => GetAppSettings<int>("ShortCodeVerificationCode");

        public static string SystemClient => GetAppSettings<string>("SystemClient");

        public static string RedisSecret => GetAppSettings<string>("RedisSecret");

        public static string BaseUrl => GetAppSettings<string>("BaseUrl");

        public static string GetRedisConnectionString => GetAppSettings<string>("Redis", "ConnectionStrings");

        public static LogLevel LogLevel => LogLevel.Debug;

        public static string GetConnectionString(string connectionString = "")
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = GetAppSettings<string>("DefaultConnection", "ConnectionStrings");
            }
            return connectionString;
        }
       
    }

}