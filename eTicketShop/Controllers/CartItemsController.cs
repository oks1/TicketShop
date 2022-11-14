using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eTicketShop.Areas.Identity.Data;
using eTicketShop.Interface;
using eTicketShop.Data.Cart;
using eTicketShop.Data.ViewModels;
using System.Reflection.Metadata;

namespace eTicketShop.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly TicketShopDB2Context _context;
        private readonly IEvent _event;
        private readonly ShoppingCart _shoppingCart;


        public CartItemsController (TicketShopDB2Context context, IEvent @event, ShoppingCart shoppingCart)
        {
            _context = context;
            _event = @event;
            _shoppingCart = shoppingCart;
        }

              

        public IActionResult ShoppingCart()
        {
            List<Models.ShoppingCartItem> items = _shoppingCart.GetShoppingCartItem();
            _shoppingCart.ShoppingCartItem = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
           //var item = _event.FirstOrDefaultAsync(i => i.Id == id);
            var item = await _event.GetEventByIdAsync(id);


            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _event.GetEventByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
