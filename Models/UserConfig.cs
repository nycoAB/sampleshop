using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyShopApi.Models
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.PasswordSalt)
               .IsRequired()
               .HasMaxLength(256);

            builder.Property(p => p.PasswordHash)
               .IsRequired()
               .HasMaxLength(512);


        }
    }
}
