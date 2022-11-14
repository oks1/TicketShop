using eTicketShop.Areas.Identity.Data;

namespace eTicketShop.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(string id);

        User UpdateUser(User user);
    }
}
