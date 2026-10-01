using UserService.Application.Feature.Google.Command.Model;

namespace UserService.Application.ExternalLogin
{
    public interface IGoogleAuthService
    {
        Task<GoogleUserModel> ValidateIdTokenAsync(string idToken);

    }
}
