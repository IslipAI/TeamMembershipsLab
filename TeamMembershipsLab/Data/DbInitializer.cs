using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeamMembershipsLab.Models;

namespace TeamMembershipsLab.Data
{
    /// <summary>
    /// DbInitializer class seeds users and roles
    /// into the web application.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Creates AppSecrets instance to get app secrets for user creation.
        /// </summary>
        public static AppSecrets AppSecret { get; set; }

        /// <summary>
        /// Method checks if the application has any roles and users. 
        /// </summary>
        /// <param name="serviceProvider">Interface mechanism for retrieving a service object.</param>
        /// <returns></returns>
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  // should log an error message here

            return 0;
        }

        /// <summary>
        /// Method adds roles to the database.
        /// </summary>
        /// <param name="roleManager">Manages the applications roles</param>
        /// <returns></returns>
        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Create Member Role
            result = await roleManager.CreateAsync(new IdentityRole("Player"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }

        /// <summary>
        /// Method populates database with a player and manager type user.
        /// </summary>
        /// <param name="userManager">Manages the users.</param>
        /// <returns></returns>
        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Admin User
            var managerUser = new ApplicationUser
            {
                UserName = "manager@mohawkcollege.ca",
                Email = "manager@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Manager",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(managerUser, AppSecret.AdminPass);
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(managerUser, "Manager");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Member User
            var playerUser = new ApplicationUser
            {
                UserName = "player@mohawkcollege.ca",
                Email = "player@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Player",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(playerUser, AppSecret.MemberPass);
            if (!result.Succeeded)
                return 3;  // should log an error message here

            // Assign user to Member role
            result = await userManager.AddToRoleAsync(playerUser, "Player");
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }
    }
}
