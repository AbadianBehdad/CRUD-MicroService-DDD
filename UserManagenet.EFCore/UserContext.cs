using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAgg;
using UserManagenet.EFCore.Mapping;

namespace UserManagenet.EFCore
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public UserContext(DbContextOptions options) : base(options)
        {   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblly = typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
