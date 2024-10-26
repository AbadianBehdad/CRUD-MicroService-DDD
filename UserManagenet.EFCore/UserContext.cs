using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.UserAgg;
using UserManagement.Domain.UserAgg.Security;
using UserManagenet.EFCore.Mapping;

namespace UserManagenet.EFCore
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Perimission> perimissions => Set<Perimission>();
        public DbSet<RolePerimission> rolePerimissions => Set<RolePerimission>();
        public DbSet<Role> roles => Set<Role>();
        public DbSet<UserRole> userRoles => Set<UserRole>();

        public UserContext(DbContextOptions<UserContext> options) : base(options)
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
