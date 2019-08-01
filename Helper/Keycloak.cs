using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace launchpad_backend.Helper
{
    public class KeyCloak
    {
        public static List<string> GetRoles(IEnumerable<Claim> claims)
        {
            var result = new List<string>();
            var obj = JObject.Parse(claims.FirstOrDefault(o => o.Type == "resource_access")?.Value);
            foreach (var item in obj?.Children())
            {
                var token = obj?.SelectToken($"{item.Path}.roles");
                result.AddRange(token?.Select(o => o.Value<string>()));
            }
            return result;
        }

        public static List<string> GetRealmRoles(IEnumerable<Claim> claims)
        {
            var result = new List<string>();
            var obj = JObject.Parse(claims.FirstOrDefault(o => o.Type == "realm_access")?.Value);
            var token = obj?.SelectToken($"roles");
            result.AddRange(token?.Select(o => o.Value<string>()));
            return result;
        }

        public static List<string> GetApps(IEnumerable<Claim> claims)
        {
            var result = new List<string>();
            var obj = JObject.Parse(claims.FirstOrDefault(o => o.Type == "resource_access")?.Value);
            foreach (var item in obj?.Children())
            {
                result.Add(item?.Path);
            }
            return result;
        }

        public static string GetProperty(IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(o => o.Type == type)?.Value;
        }
    }

    public static class ClaimTypes
    {
        public const string Name = "name";
        public const string Username = "preferred_username";
        public const string Client = "azp";
        public const string Givenname = System.Security.Claims.ClaimTypes.GivenName;
        public const string Surname = System.Security.Claims.ClaimTypes.Surname;
        public const string Email = System.Security.Claims.ClaimTypes.Email;
    }
}

