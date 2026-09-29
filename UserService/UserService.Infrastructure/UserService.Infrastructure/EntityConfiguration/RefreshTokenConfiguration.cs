using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.EntityConfiguration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {


            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.refreshToken).HasMaxLength(200).IsRequired();
            builder.Property(rt => rt.Expires).IsRequired();
            builder.Property(rt => rt.Created).IsRequired();
            builder.HasOne(rt => rt.User)
                  .WithMany(u => u.RefreshTokens)
                  .HasForeignKey(rt => rt.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }


    }
}

