namespace UserService.Application.dtos
{
    public class JWTAuthResponse
    {
        public RefreshTokenResponse RefreshToken { get; set; } = null!;
        public string Token { get; set; }
    }
}
