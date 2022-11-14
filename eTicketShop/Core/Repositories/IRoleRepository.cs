using eTicketShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace eTicketShop.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
