using System.ComponentModel.DataAnnotations;

namespace CrudApplication.Models
{
    public class AddEmployeeCatViewModel
    {
        [Required]
        public string CategoryName { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
