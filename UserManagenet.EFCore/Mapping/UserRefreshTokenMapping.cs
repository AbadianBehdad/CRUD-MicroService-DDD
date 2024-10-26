using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.UserAgg.Security;

namespace UserManagenet.EFCore.Mapping
{
    public class UserRefreshTokenMapping : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshToken");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.UserId).IsRequired();
            builder.Property(x=> x.RefreshToken).IsRequired();

            builder.Property(x=> x.IsValid).IsRequired();
            builder.Property(x=> x.RefreshTokenTimeout).IsRequired();
            builder.Property(x=> x.CreationDate).IsRequired();


        }
    }
}
