using eTicketShop.Areas.Identity.Data;
using eTicketShop.Data.Base;
using eTicketShop.Data.ViewModels;
using eTicketShop.Interface;
using eTicketShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Interface
{
    public class EventService : EntityBaseRepository<Event>, IEvent
    {
        private readonly TicketShopDB2Context _context;
        public EventService(TicketShopDB2Context context) : base(context)
        {
            _context = context;
        }

        

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var eventDetails = await _context.Events
                .Include(c => c.Category)
                .FirstOrDefaultAsync(n => n.Id == id);

            return eventDetails;
        }

       
    }
}
