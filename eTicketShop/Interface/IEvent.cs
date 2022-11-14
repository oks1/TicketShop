using eTicketShop.Data.Base;
using eTicketShop.Data.ViewModels;
using eTicketShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Interface

{
    public interface IEvent : IEntityBaseRepository<Event>
    {
        Task<Event> GetEventByIdAsync(int id);
        //Task<NewEventDropdownsVM> GetNewEventDropdownsValues();
       // Task AddNewMovieAsync(NewEventVM data);
       // Task UpdateMovieAsync(NewEventVM data);
    }
}
