using Microsoft.AspNetCore.Identity;
using PROG7311_ST10263164.Data;
using PROG7311_ST10263164.Models;

namespace PROG7311_ST10263164.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider) 
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                logger.LogInformation("Ensuring database created");
                await context.Database.EnsureCreatedAsync();

                logger.LogInformation("Seeding roles");
                await AddRoleAsync(roleManager, "Employee");
                await AddRoleAsync(roleManager, "Farmer");

                logger.LogInformation("Seed employee");
                var employeeEmail = "mb@gmail.com";
                if (await userManager.FindByEmailAsync(employeeEmail) == null)
                {
                    var employeeUser = new Users
                    {
                        UserName = employeeEmail,
                        Email = employeeEmail,
                        FullName = "Brummer,Marli",
                        NormalizedUserName = employeeEmail.ToUpper(),
                        NormalizedEmail = employeeEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };

                    var result = await userManager.CreateAsync(employeeUser, "Password123!");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    Console.WriteLine($"Role {roleName} created successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

    }
}
