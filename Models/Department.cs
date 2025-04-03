using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAuth.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepName { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepLocation { get; set; }

        // Navigation Property
        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
