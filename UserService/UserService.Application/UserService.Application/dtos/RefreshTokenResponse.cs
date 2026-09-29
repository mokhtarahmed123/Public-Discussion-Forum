namespace UserService.Application.dtos
{
    public class RefreshTokenResponse
    {
        public string RefreshToken { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
