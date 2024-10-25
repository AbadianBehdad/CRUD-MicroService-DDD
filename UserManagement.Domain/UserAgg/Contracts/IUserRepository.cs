using Framwork.Domain;

namespace UserManagement.Domain.UserAgg.Contracts
{
    public interface IUserRepository : IRepository<Guid, User>
    {
        Task<User> GetBy(string name);
        
    }
}
