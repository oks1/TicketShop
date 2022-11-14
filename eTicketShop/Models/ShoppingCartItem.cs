using System.ComponentModel.DataAnnotations;

namespace eTicketShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Event Event { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
