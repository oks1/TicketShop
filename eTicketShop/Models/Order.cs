using eTicketShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTicketShop.Models
{
    public class Order
    {

        public int Id { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        [Range(0.00, Double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(250)]
        public string PaymentCode { get; set; }
        public DateTime PaidDateTime { get; set; }


        public List<OrderItem> OrderItems { get; set; }
        //public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
