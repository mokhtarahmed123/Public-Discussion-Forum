namespace UserService.Application.dtos
{
    public class JWTModel
    {
        public string AudienceIP { get; set; }
        public string IssuerIP { get; set; }
        public string SecretKey { get; set; } = null!;
        public int AccessTokenExpiredDate { get; set; }
        public int RefreshTokenExpiredDate { get; set; }
    }
}
