using eTicketShop.Areas.Identity.Data;
using eTicketShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Interface
{
    public class OrdersService : IOrder
    {
        private readonly TicketShopDB2Context _context;
        public OrdersService(TicketShopDB2Context context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Event).Include(n => n.User).ToListAsync();

            if(userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, decimal totalPrice, string? paymentCode, DateTime paidDateTime
            )
            
        {
            var order = new Order()
            {
                UserId = userId,
                TotalPrice = items.Sum(x=>x.Amount* x.Event.Price),
                PaymentCode = "paycode01",
                PaidDateTime = DateTime.Now,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    EventId = item.Event.Id,
                    OrderId = order.Id,
                    Price = item.Event.Price,
                    Qty = item.Amount

                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
