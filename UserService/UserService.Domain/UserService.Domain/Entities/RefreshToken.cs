namespace UserService.Domain.Entities
{
    public class RefreshToken
    {
        //[Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        //[MaxLength(200)]
        public string refreshToken { get; set; } = string.Empty;

        public DateTime Expires { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Revoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;

        public Guid UserId { get; set; }

        public Users User { get; set; }
    }
}
