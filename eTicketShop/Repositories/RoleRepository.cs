using eTicketShop.Areas.Identity.Data;
using eTicketShop.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace eTicketShop.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TicketShopDB2Context _context;

        public RoleRepository(TicketShopDB2Context context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
