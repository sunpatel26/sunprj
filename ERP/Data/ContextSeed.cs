using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models;

namespace ERP.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(ERP.Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERP.Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERP.Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERP.Enums.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "haresh@gmail.com",
                FirstName = "Haresh",
                LastName = "Kyada",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, ERP.Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, ERP.Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, ERP.Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, ERP.Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
