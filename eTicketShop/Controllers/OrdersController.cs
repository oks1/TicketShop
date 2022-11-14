using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eTicketShop.Areas.Identity.Data;
using eTicketShop.Models;
using eTicketShop.Data.ViewModels;
using System.Security.Claims;
using eTicketShop.Data.Cart;
using eTicketShop.Interface;


namespace eTicketShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TicketShopDB2Context _context;
        private readonly ShoppingCart _shoppingCart;
//private readonly EventsController _event;
       // private readonly OrdersController _order;
       private readonly IEvent _event;
        private readonly IOrder _order;
        private readonly string? paymentCode;
      private readonly DateTime paidDateTime;
     private readonly decimal totalPrice;

        public OrdersController(TicketShopDB2Context context, ShoppingCart shoppingCart, IOrder ordersService, IEvent eventService)
        {
            _context = context;
            _shoppingCart = shoppingCart;
           _order = ordersService;
            _event = eventService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _order.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Event).Include(n => n.User).ToListAsync();
            return View(orders);
            //return View(await _context.Orders.Include(n => n.User).ToListAsync ());
        }
        public IActionResult ShoppingCart()
        {
           var items = _shoppingCart.GetShoppingCartItem();
            _shoppingCart.ShoppingCartItem = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        //// GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderitems = await _context.OrderItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderitems == null)
            {
                return NotFound();
            }

            return View(orderitems);
        }

        // GET: Orders/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,TotalPrice,PaymentCode,PaidDateTime")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        // GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TotalPrice,PaymentCode,PaidDateTime")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Orders == null)
        //    {
        //        return Problem("Entity set 'TicketShopDB2Context.Orders'  is null.");
        //    }
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId,
        //    decimal totalPrice, string? paymentCode, DateTime paidDateTime)
        //{
        //    var order = new Order()
        //    {
        //        UserId = userId,
        //        TotalPrice = totalPrice,
        //        PaymentCode = paymentCode,
        //        PaidDateTime = paidDateTime

        //    };
        //    await _context.Orders.AddAsync(order);
        //    await _context.SaveChangesAsync();

        //    foreach (var item in items)
        //    {
        //        var orderItem = new OrderItem()
        //        {
        //            EventId = item.Event.Id,
        //            OrderId = order.Id,
        //            Price = item.Event.Price,
        //            Qty = item.Amount,
        //        };
        //        await _context.OrderItems.AddAsync(orderItem);
        //    }
        //    await _context.SaveChangesAsync();
        //}


        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _event.GetEventByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

      
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItem();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            await _order.StoreOrderAsync(items, userId, totalPrice, paymentCode, paidDateTime);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
