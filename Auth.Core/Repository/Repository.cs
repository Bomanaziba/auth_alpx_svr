

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Auth.Core.Common;
using Microsoft.Extensions.Logging;

namespace Auth.Core.Repository
{
    public class Repository
    {

        private readonly static ILogger _logger = new LoggerFactory().CreateLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);


        public static async Task<bool> PingSQL()
        {
            try
            {
                using (var connStr = new SqlConnection(AppSetting.GetConnectionString()))
                {
                     await connStr.OpenAsync();
                     await connStr.CloseAsync();

                     return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private static PropertyInfo[] GetObjectProp<T>()
        {
            PropertyInfo[] propertyInfos;

            propertyInfos = typeof(T).GetProperties();

            Array.Sort(propertyInfos,
            delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            return propertyInfos;

        }

        private static SqlParameter[] GetParameter<T>(T parameter)
        {
            var sqlParamCollection = new List<SqlParameter>();

            var propertyInfos = GetObjectProp<T>();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var objName = propertyInfo.Name;

                var objValue = parameter.GetType().GetProperty(propertyInfo.Name).GetValue(parameter, null);

                var sqlParam = new SqlParameter($"@{objName}", objValue);

                if(objValue == null) continue;

                sqlParamCollection.Add(sqlParam);
            }

            return sqlParamCollection.ToArray();
        }
        private static T ReturnValue<T>(object value)
        {
            if (value == null || value is DBNull) return default(T);

            var returnType = typeof(T);
            var dataType = value.GetType();

            if (returnType != dataType) return (T)Convert.ChangeType(value, returnType);

            return (T)value;
        }

        private static List<T> ReadCollection<T>(SqlDataReader reader)
        {
            var resp = Activator.CreateInstance<List<T>>();

            while(reader.Read())
            {
                resp.Add(ReadSingle<T>(reader));
            }

            return resp;
        }
        
        private static T ReadSingle<T>(SqlDataReader reader)
        {
            var result = Activator.CreateInstance<T>();

            var parameters = GetObjectProp<T>();

            foreach(var item in parameters)
            {
                var value = reader[item.Name];
                if(value is DBNull) continue;

                var pInfo = result.GetType().GetProperty(item.Name);
                if(pInfo == null) continue;

                var vType = value.GetType();
                if(vType != pInfo.PropertyType)
                {
                    value = Convert.ChangeType(value, pInfo.PropertyType);
                }

                pInfo.SetValue(result, value, null);
            }

            return result;
        }
        
        #region Generic Read and Write to Db
        public static async Task<T> Write<T, TK>(TK parameter, string command, CommandType commandType, string connectionString)
        {
            var result = Activator.CreateInstance<T>();

            try
            {
                using (var connStr = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(command, connStr))
                    {
                        connStr.Open();

                        if(parameter != null)
                        {
                            cmd.Parameters.AddRange(GetParameter(parameter));
                        }

                        cmd.CommandType = commandType;

                        var resp = await cmd.ExecuteScalarAsync();

                        connStr.Close();

                        result = ReturnValue<T>(resp);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static async Task<T> Read<T, TK>(TK parameter, string command, CommandType commandType, string connectionString)
        {
            var result = default(T);

            try
            {
                using (var connStr = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(command, connStr))
                    {
                        connStr.Open();

                        if(parameter != null)
                        {
                            cmd.Parameters.AddRange(GetParameter(parameter));
                        }

                        cmd.CommandType = commandType;

                        using(var reader = await cmd.ExecuteReaderAsync())
                        {

                            reader.Read();
                            
                            if(reader.HasRows)
                            {
                                var resp = ReadSingle<T>(reader);

                                result = ReturnValue<T>(resp);
                            }
                            
                        }

                        connStr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return result;
        }

        public static async Task<IList<T>> ReadList<T, TK>(string command, CommandType commandType, string connectionString, TK parameter = default(TK))
        {
            var result = Activator.CreateInstance<List<T>>();

            try
            {
                using (var connStr = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(command, connStr))
                    {
                        connStr.Open();

                        if(parameter != null)
                        {
                            cmd.Parameters.AddRange(GetParameter(parameter));
                        }

                        cmd.CommandType = commandType;

                        using(var reader = await cmd.ExecuteReaderAsync())
                        {

                            var resp = ReadCollection<T>(reader);
                            
                            result = ReturnValue<List<T>>(resp);
                        }

                        connStr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
            
        }
        #endregion
    
    }

}