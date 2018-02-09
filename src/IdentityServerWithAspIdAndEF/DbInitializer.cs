using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF
{
    public static class DbInitializer
    {
        // Class used for seeding Roles and Users of the system, as well as
        // adding users to the Roles seeded.

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("user1@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user1@gmail.com";
                user.Email = "user1@gmail.com";
                user.Description = "SomethingaboutUser1";
                user.FirstName = "User1FirstName";
                user.LastName = "User1LastName";

                IdentityResult result = userManager.CreateAsync
                (user, "PassW0rd!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                    "NormalUser").Wait();
                }
            }


            if (userManager.FindByNameAsync("user2@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user2@gmail.com";
                user.Email = "user2@gmail.com";
                user.Description = "somethingaboutUser2";
                user.FirstName = "User2FirstName";
                user.LastName = "User2LastName";
                IdentityResult result = userManager.CreateAsync
                (user, "PassW0rd!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                    "Administrator").Wait();
                }
            }
        }
    }
}
        




            