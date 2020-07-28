using System;
using System.Linq;
using System.Security.Claims;

namespace Cmta.Clients.Spa.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasClaim(this ClaimsPrincipal user, string type)
        {
            var claim = user.Claims.FirstOrDefault(e => e.Type == type);
            return claim != null;
        }
        
        public static string GetClaim(this ClaimsPrincipal user, string type)
        {
            var claim = user.Claims.FirstOrDefault(e => e.Type == type);
            return claim?.Value;
        }

        public static Guid GetTenantId(this ClaimsPrincipal user)
        {
            try
            {
                return Guid.Parse(GetClaim(user, "tenant_id"));
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}