using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace FeatureDemo.Api.Utilities
{
    public static class AppSettings
    {

        public static IConfiguration Configuration { get; set; }
        public static IHostingEnvironment Environment { get; set; }

        static object GetConnfiguration(string name, object defaultValue = null)
        {
            return Configuration?.GetValue<object>(name) ?? defaultValue;
        }

        static string GetConnectionString([CallerMemberName] string name = "", string defalutValue = "")
        {
            return Configuration?.GetConnectionString(name)?.ToString() ?? defalutValue;
        }

        static TResult GetProperty<TResult>(Func<Object, TResult> func, TResult defaultValue = default(TResult), [CallerMemberName] string name = "") where TResult : IConvertible
        {
            var configVal = GetConnfiguration(name);
            if (configVal == null || func == null) return defaultValue;
            try
            {
                return func.Invoke(configVal);
            }
            catch
            {
                return defaultValue;
            }
        }

        static TResult GetProperty<TResult>(TResult defaultValue = default(TResult), [CallerMemberName] string name = "") where TResult : IConvertible
        {
            var configValue = GetConnfiguration(name);
            if (configValue == null)
                return defaultValue;

            var typeCode = defaultValue.GetTypeCode();
            switch (typeCode)
            {
                case TypeCode.Int32:
                    try
                    {
                        var val = Convert.ToInt32(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                case TypeCode.Int64:
                    try
                    {
                        var val = Convert.ToInt64(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                case TypeCode.String:
                    try
                    {
                        var val = Convert.ToString(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                case TypeCode.Decimal:
                    try
                    {
                        var val = Convert.ToDecimal(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                case TypeCode.Double:
                    try
                    {
                        var val = Convert.ToDouble(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }
                case TypeCode.DateTime:
                    try
                    {
                        var val = Convert.ToDateTime(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                case TypeCode.Boolean:
                    try
                    {
                        var val = Convert.ToBoolean(configValue);
                        return (TResult)Convert.ChangeType(val, typeof(TResult));
                    }
                    catch
                    {
                        return defaultValue;
                    }

                default:
                    return defaultValue;
            }
        }

        public static string FeatureDatabase => GetConnectionString();

        public static int MaxBatchSize => GetProperty(500);

        public static string InstitutionHeader => "Institution";
    }
}
