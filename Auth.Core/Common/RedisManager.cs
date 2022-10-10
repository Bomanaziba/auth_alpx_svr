
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Auth.Core.Common
{

    public class RedisManger
    {
        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private static IDatabase Db()
        {
            try
            {
                var command = new Dictionary<string, string>
                {
                    { "info", null },
                    { "select", "use" }
                };

                Lazy<ConnectionMultiplexer> redis = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    AsyncTimeout = 30000,
                    AbortOnConnectFail = false,
                    KeepAlive = 4,
                    AllowAdmin = true,
                    CommandMap = CommandMap.Create(command),
                    EndPoints = { AppSetting.GetRedisConnectionString },
                    Password = AppSetting.RedisSecret
                }, Console.Out));

                return redis.Value.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public static async Task<bool> PingRedis()
        {
            try
            {
                await Db().PingAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public static async Task<T> GetRedis<T>(string key)
        {
            try
            {
                var resp = await Db().StringGetAsync(key);

                if(!resp.HasValue) return default(T);

                return JsonSerializer.Deserialize<T>(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public static async Task SetRedis<T>(string key, T data, TimeSpan time)
        {
            try
            {
                await Db().StringSetAsync(key, JsonSerializer.Serialize(data), time);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public static async Task DeleteRedis(string key)
        {
            try
            {
                await Db().KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }

}