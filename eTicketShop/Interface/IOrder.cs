using eTicketShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Interface
{
    public interface IOrder
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, decimal totalPrice, string paymentCode, DateTime paidDateTime);
       // decimal TotalPrice, string paymentCode, DateTime PaidDateTime
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
        //Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string? paymentCode, decimal totalPrice, DateTime paidDateTime);
    }
}
