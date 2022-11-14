using System.ComponentModel.DataAnnotations;

namespace eTicketShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name should be between 2 and 100 characters")]
        public string Name { get; set; }

        public ICollection<Event>? Events { get; set; }
    }
}
