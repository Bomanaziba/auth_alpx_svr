using System;
using System.Collections.Generic;
using System.Text;
using Auth.Core.Common;
using Auth.Core.Factory;
using DbUp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Auth.API
{
    public class Startup
    {
        private ILogger<Startup> _logger { get; set; }

        public Startup(IWebHostEnvironment env, ILogger<Startup> logger) 
        {

            _logger = logger;
            
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }
            else
            {
                //builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.JwtToken));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "AlphaX",
                    ValidAudience = "AlphaX",
                    IssuerSigningKey = symmetricSecurityKey
                };

                
            
            });

            services.AddAuthorization(options =>
                options.AddPolicy("ValidAccessToken", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                }
            ));
            

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "AccountServiceSession";
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    AsyncTimeout = 30000,
                    AbortOnConnectFail = false,
                    KeepAlive = 4,
                    AllowAdmin = true,
                    CommandMap = CommandMap.Create(new Dictionary<string, string>
                {
                    { "info", null },
                    { "select", "use" }
                }),
                    EndPoints = { Configuration.GetConnectionString("Redis") },
                    Password = AppSetting.RedisSecret
                };
            });

            services.AddSession(options =>
            {
                 options.IdleTimeout = TimeSpan.FromMinutes(30);
                 options.Cookie.IsEssential = true;
            });

            services.AddSwaggerGen();

            services.AddControllers();

            services.AddMemoryCache();

            services.AddSingleton(Configuration);

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddCheck<HealthyCheck>("");
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz");
            });

            loggerFactory = LoggerFactory.Create(
                builder => builder
                .AddConsole()
                .AddDebug()

            );

            MigrationScript();
        }

        public void MigrationScript()
        {
            string dbConStr = Configuration.GetConnectionString("DefaultConnection");

            var upgrader = DeployChanges.To.SqlDatabase(dbConStr)
            .WithScriptsFromFileSystem($"SQL")
            .WithTransactionPerScript()
            .JournalToSqlTable("dbo", "MigrationsJournal")
            .LogToConsole()
            .LogScriptOutput()
            .Build();

            upgrader.PerformUpgrade();
        }
    }
}
