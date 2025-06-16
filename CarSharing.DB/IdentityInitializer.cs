using CarSharing.DB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.DB
{
    internal class IdentityInitializer
    {
        public static void Initialize(UserManager<UserDB> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "_admin@sss.ru";
            var adminPassword = "_Aa123456";

            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
            }

            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
            }

            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new UserDB { Email = adminEmail, UserName = adminEmail };
                if (userManager.CreateAsync(admin, adminPassword).Result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
                }
            }


        }
    }
}
