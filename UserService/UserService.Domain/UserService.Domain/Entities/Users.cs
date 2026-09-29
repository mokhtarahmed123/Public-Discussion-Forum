using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Entities
{
    public class Users : IdentityUser<Guid>
    {
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActived { get; set; } = false;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();


    }
}
