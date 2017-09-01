using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FeatureDemo.Identity.Utilities
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

        static TResult GetProperty<TResult>(Func<Object, TResult> func, TResult defaultValue = default(TResult), [CallerMemberName] string name = "")
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

        public static string AuthDatabase => GetConnectionString();

        public static int MaxBatchSize => GetProperty(500);

        public static string InstitutionHeader => "X-ClientId";

        public static SymmetricSecurityKey SecurityKey => GetProperty(key => new SymmetricSecurityKey(Encoding.ASCII.GetBytes((string)key)));

        public static RsaSecurityKey RsaSecurityKey => new RsaSecurityKey(new RSACryptoServiceProvider(2048));

        public static List<TestUser> TestUsers => new List<TestUser>() 
        { 
            new TestUser { SubjectId = "1", Username = "user1", Password = "password1", Claims = { new Claim(JwtClaimTypes.Email, "user1@nwcu.com"), new Claim(JwtClaimTypes.Role, "admin") } }, 
            new TestUser { SubjectId = "2", Username = "user2", Password = "password2", Claims = { new Claim(JwtClaimTypes.Email, "user2@nwcu.com"), new Claim(JwtClaimTypes.Role, "role") } } 
        };

        public static List<ApiResource> ApiResources => new List<ApiResource>() 
        { 
            new ApiResource { Name = "api1", DisplayName = "FeatureDemo Api", Description = "FeatureDemo Api Access", UserClaims = { "role" }, ApiSecrets = { new Secret("scopeSecrit".Sha256())}, Scopes = { new Scope("api1.read"), new Scope("api1.write") } } 
        };

        public static List<IdentityResource> IdentityResources => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(), new IdentityResources.Profile(), new IdentityResources.Email(), new IdentityResource { Name = "role", UserClaims = { "role" } }
        };

        public static List<Client> ApiClients => new List<Client>() 
        { 
            new Client { ClientId = "fd.client", AllowedGrantTypes = new List<string> { GrantType.ResourceOwnerPassword }, ClientSecrets = { new Secret("secret".Sha256()) }, AllowedScopes = { "api1" } } 
        };
    }
}
