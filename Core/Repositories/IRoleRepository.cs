using University.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace University.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
