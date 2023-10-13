
namespace CrudApplication.Models
{
    public class DisplayEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public string WorkStatus { get; set; }
        public string StaffId { get; set; } 
        public string PhoneNumber { get; set; }  
        public DateTime EmployeeDateCreated { get; set; } = DateTime.Now;
        public string CategoryName { get; set; }

    }
}
