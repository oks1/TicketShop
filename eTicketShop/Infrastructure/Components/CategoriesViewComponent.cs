using eTicketShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicketShop.Infrastructure.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly TicketShopDB2Context _context;

        public CategoriesViewComponent(TicketShopDB2Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());
    }
}
