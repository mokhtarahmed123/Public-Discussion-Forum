using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.EntityConfiguration;

namespace UserService.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<Users, Role, Guid>
    {
        #region Tables
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion


        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}