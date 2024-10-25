using Framwork.Infrastructre;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAgg;
using UserManagement.Domain.UserAgg.Contracts;

namespace UserManagenet.EFCore.Repository
{
    public class UserRepository : RepositoryBase<Guid, User> , IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext context) : base(context)
        {
            _userContext = context;
        }

        public async Task<User> GetBy(string name)
        {
            return await _userContext.Users.SingleOrDefaultAsync(u => u.Name == name);
        }
    }
}
