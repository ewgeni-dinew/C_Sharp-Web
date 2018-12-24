using BabyBug.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBug.Web.Utilities
{
    internal static class RoleSeeder
    {
        internal static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //Initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<BabyBugUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //Create the roles and seed them to the database
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Create admin user
            var poweruser = new BabyBugUser
            {
                UserName = configuration["AppSettings:Email"],
                Email = configuration["AppSettings:Email"],
                FirstName = configuration["AppSettings:FirstName"],
                LastName = configuration["AppSettings:LastName"],
            };

            //Assign password
            string userPassword = configuration["AppSettings:Password"];

            var _user = await userManager.FindByEmailAsync(configuration["AppSettings:Email"]);

            if (_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //Assign administrator role
                    await userManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}
