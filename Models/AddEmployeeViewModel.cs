using System.ComponentModel.DataAnnotations;

namespace CrudApplication.Models
{
    public class AddEmployeeViewModel
    {

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

        //public string StaffId { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = "";

        public DateTime EmployeeDateCreated { get; set; } = DateTime.Now;

        public int CategoryTableId { get; set; }

        //[Required]
        //public string CategoryName { get; set; }

    }
}
