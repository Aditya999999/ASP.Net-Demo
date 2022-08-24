using System;
using System.Threading.Tasks;
using DP.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace DP.Web.Data
{
    // A Singleton Class
    public class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<IdentityRole> rolemanager)
        {
            foreach(MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                //To Check that role exists already
                if(!await rolemanager.RoleExistsAsync(rolename.ToString()))
                {
                    //If not found then create new role
                    await rolemanager.CreateAsync(
                        new IdentityRole { Name = rolename.ToString()});
                }
            }
        }
    public static async Task SeedIdentityUserAsync(UserManager<IdentityUser> usermanager)
    {
            IdentityUser user;
            user = await usermanager.FindByNameAsync("admin@dp.com");
            if(user == null)
            {
                user = new IdentityUser()
                {
                    //Credential for Admin
                    UserName = "admin@dp.com",
                    Email = "admin@dp.com",
                    EmailConfirmed = true,
                };
                await usermanager.CreateAsync(user, password: "Aditya@123");

                await usermanager.AddToRolesAsync(user, new string[]
                {
                    MyIdentityRoleNames.AppAdmin.ToString(),
                    MyIdentityRoleNames.Citizen.ToString(),
                    MyIdentityRoleNames.Policemen.ToString()
                });
            }

            user = await usermanager.FindByNameAsync("user@dp.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "user@dp.com",
                    Email = "user@dp.com",
                    EmailConfirmed = true

                };

                await usermanager.CreateAsync(user, password: "Aditya@123");

                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.Citizen.ToString());
            }
        }
    }
}
