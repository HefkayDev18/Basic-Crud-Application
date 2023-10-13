using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models
{
    public class Employee
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string WorkStatus { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string StaffId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime EmployeeDateCreated { get; set; } 

        [ForeignKey("CategoryTableId")]
        public Categories Category { get; set; }

    }
}
