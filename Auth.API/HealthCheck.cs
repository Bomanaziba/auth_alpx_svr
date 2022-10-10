using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Repository;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Auth.API
{
    public class HealthyCheck : IHealthCheck
    {
        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = false;
            
            try
            {
                if (await Repository.PingSQL() && await MongoManger.PingMongo() && await RedisManger.PingRedis()) isHealthy = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                isHealthy = false;
            }

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("");
            }

            return HealthCheckResult.Unhealthy("");
        }
    }
}