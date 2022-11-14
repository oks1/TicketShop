using eTicketShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eTicketShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity must be greater than 1")]
        public int Qty { get; set; }

        public Event Event { get; set; }
    }
}
