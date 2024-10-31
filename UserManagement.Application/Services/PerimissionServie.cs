using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UserManagement.Application.Interfaces;
using UserManagenet.EFCore;

namespace UserManagement.Application.Services
{
    public class PerimissionServie : IPerimissionServices
    {
        private readonly UserContext _userContext;
        private readonly IMemoryCache _memoryCache;

        public PerimissionServie(UserContext userContext, IMemoryCache memoryCache)
        {
            _userContext = userContext;
            _memoryCache = memoryCache;
        }
        public async Task<bool> CheckPerimission(Guid userId, string perimission)
        {
            var permissionFlags = new List<string>();
            var permissionCacheKey = $"Perimission-{userId.ToString()}";

            if (!_memoryCache.TryGetValue(permissionCacheKey, out permissionFlags))
            {
                var roles = await _userContext.userRoles.Where(x => x.UserID == userId).Select(x => x.RoleId).ToListAsync();

                permissionFlags = await _userContext.rolePerimissions
               .Where(q => roles.Contains(q.RoleID)).Select(q => q.Perimission.PerimissionFlag).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _memoryCache.Set(permissionCacheKey,permissionFlags, cacheEntryOptions);

            }

            return permissionFlags.Contains(perimission);
        }
    }
}
