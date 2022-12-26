using Business.Entities;
using ERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP.Helpers
{
    public static class ClaimsHelper
    {
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        public static UserMasterMetadata GetActiveUser()
        {
            UserMasterMetadata response = null;
          
            if (IsUserAuthenticated())
            {
                var claim = _httpContext.User.Claims.FirstOrDefault(s => s.Type.Equals(ClaimTypes.UserData));

                if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
                {
                    response = JsonConvert.DeserializeObject<UserMasterMetadata>(claim.Value);
                }
            }
            return response;
        }

        
        public static bool IsUserAuthenticated()
        {
            return _httpContext.User.Identity.IsAuthenticated;
        }

        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            }
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
