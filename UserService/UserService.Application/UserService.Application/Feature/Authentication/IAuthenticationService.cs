using UserService.Application.dtos;
using UserService.Domain.Entities;
using UserService.Infrastructure.dtos;

namespace UserService.Application.Feature.Authentication
{
    public interface IAuthenticationService
    {
        public Task<JWTAuthResponse> GenerateJWToken(Users user);

        public Task SaveRefreshToken(RefreshToken refreshToken, Users user);
        public Task<JWTAuthResponse> GetRefreshToken(string refreshTokenString, string Token);
        Task RevokeRefreshToken(Guid UserId);
        public Task<SignUpResult> SignUpAsync(Users user, string Password);
        public Task<ConfirmEmailResult> ConfirmEmail(Guid UserId, string Code);
        public Task<SendResetPasswordCodeResult> SendResetPasswordCode(string email);
        public Task<ResetPasswordResult> ResetPasswordCode(string email, string Password);
        public Task<ConfirmResetPasswordResult> ConfirmResetPassword(string Code, string Email);
        public Task<string> ValidateToken(string Token);
    }
}
