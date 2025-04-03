using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemAuth.Models
{
    public class EmployeeManagementSystemContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EmployeeManagementSystemContext(DbContextOptions<EmployeeManagementSystemContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepId);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            modelBuilder.Entity<Department>().HasData(
                new Department { DepId = 1, DepName = "HR", DepLocation = "New York" },
                new Department { DepId = 2, DepName = "IT", DepLocation = "San Francisco" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmpId = 1,
                    EmpName = "John Doe",
                    JobName = "Manager",
                    ManagerId = null,
                    HireDate = new DateTime(2024, 01, 15),  // Static Date
                    Salary = 75000,
                    DepId = 1
                },
                new Employee
                {
                    EmpId = 2,
                    EmpName = "Jane Smith",
                    JobName = "Software Engineer",
                    ManagerId = 1,
                    HireDate = new DateTime(2023, 05, 10),  // Static Date
                    Salary = 85000,
                    DepId = 2
                }
            );
        }
    }
}
