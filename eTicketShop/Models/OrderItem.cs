using System.ComponentModel.DataAnnotations;

namespace eTicketShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int OrderId { get; set; }

        [Required]
        [Range(0.00, Double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }


        public int Qty { get; set; }

        public Event? Event { get; set; }
        public Order? Order { get; set; }
    }
}
