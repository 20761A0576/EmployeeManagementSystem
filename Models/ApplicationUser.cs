using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystemAuth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
