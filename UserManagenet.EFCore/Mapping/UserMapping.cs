using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.UserAgg;

namespace UserManagenet.EFCore.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(255).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(64).IsRequired();
            builder.Property(t => t.PasswordSalt).IsRequired();
            builder.Property(t => t.LastLogin).IsRequired();
            builder.Property(t => t.RegisterDate).IsRequired();


        }
    }
}
