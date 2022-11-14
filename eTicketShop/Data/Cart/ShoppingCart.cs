using eTicketShop.Areas.Identity.Data;
using eTicketShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Data.Cart
{
    public class ShoppingCart
    {
       
        public TicketShopDB2Context _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItem { get; set; }

        public ShoppingCart(TicketShopDB2Context context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<TicketShopDB2Context>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Event event1)
        {
             var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Event.Id == event1.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()

                {
                    ShoppingCartId = ShoppingCartId,
                    Event = event1,
                    Amount = 1
                };
        _context.ShoppingCartItem.Add(shoppingCartItem);
            } else
            {
                shoppingCartItem.Amount++;
            }
_context.SaveChanges();
        }

        public void RemoveItemFromCart(Event event1)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Event.Id == event1.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItem()
        {
            return ShoppingCartItem ?? (ShoppingCartItem = _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Event).ToList());

           // return _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Event).ToList();
        }

        public double GetShoppingCartTotal() => (double)_context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Event.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

      
    }
}
