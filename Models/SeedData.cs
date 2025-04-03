using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystemAuth.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                string[] roleNames = { "Admin", "Manager", "Employee" };

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                    }
                }
            }
        }
    }
}
