using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagenet.EFCore;

namespace UserManagement.Application.Services
{
    public class PerimissionServie : IPerimissionServices
    {
        private readonly UserContext _userContext;

        public PerimissionServie(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<bool> CheckPerimission(Guid userId, string perimission)
        {
            var permissionFlags = new List<string>();
            var roles = await _userContext.userRoles.Where(x=> x.UserID == userId).Select(x=> x.RoleId).ToListAsync();

            permissionFlags = await _userContext.rolePerimissions
           .Where(q => roles.Contains(q.RoleID)).Select(q => q.Perimission.PerimissionFlag).ToListAsync();

            return permissionFlags.Contains(perimission);
        }
    }
}
