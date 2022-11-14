using eTicketShop.Areas.Identity.Data;
using eTicketShop.Core.Repositories;

namespace eTicketShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketShopDB2Context _context;

        public UserRepository(TicketShopDB2Context context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(User user)
        {
             _context.Update(user);
             _context.SaveChanges();

             return user;
        }

    }
}
