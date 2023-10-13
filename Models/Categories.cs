using System.ComponentModel.DataAnnotations;

namespace CrudApplication.Models
{
    public class Categories
    {
        [Key]
        public int CategoryTableId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
