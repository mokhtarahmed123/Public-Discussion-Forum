using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.EntityConfiguration
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(u => u.Code).IsRequired();
            builder.Property(u => u.FullName).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
        }
    }
}
