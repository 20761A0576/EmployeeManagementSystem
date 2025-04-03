using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAuth.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmpName { get; set; }

        [Required]
        [MaxLength(50)]
        public string JobName { get; set; }

        public int? ManagerId { get; set; } 

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        [Required]
        public int DepId { get; set; }

        [ForeignKey("DepId")]
        public Department? Department { get; set; }
    }
}
