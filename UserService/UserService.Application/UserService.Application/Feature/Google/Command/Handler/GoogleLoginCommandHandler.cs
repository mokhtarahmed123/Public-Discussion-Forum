using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.dtos;
using UserService.Application.ExternalLogin;
using UserService.Application.Feature.Authentication;
using UserService.Application.Feature.Google.Command.Model;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Google.Command.Handler
{
    public class GoogleLoginCommandHandler : ResponseHandler, IRequestHandler<GoogleLoginCommand, Response<JWTAuthResponse>>
    {
        private readonly UserManager<Users> userManager;

        private readonly IAuthenticationService authentication;

        private readonly IGoogleAuthService googleAuthService;

        public GoogleLoginCommandHandler(UserManager<Users> userManager, IAuthenticationService authentication,
     IGoogleAuthService googleAuthService)
        {
            this.userManager = userManager;

            this.authentication = authentication;

            this.googleAuthService = googleAuthService;
        }

        public async Task<Response<JWTAuthResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {

            var googleUser = await googleAuthService.ValidateIdTokenAsync(request.IdToken);

            if (!googleUser.EmailVerified)
                throw new UnauthorizedAccessException("Google email not verified");


            var user = await userManager.FindByEmailAsync(googleUser.Email);

            if (user == null)
            {

                user = new Users
                {
                    UserName = googleUser.Email,
                    Email = googleUser.Email,
                    FullName = googleUser.Name,
                    EmailConfirmed = true,
                    Provider = "Google",
                    ExternalId = googleUser.Id,
                    IsActived = true
                };

                var createResult = await userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user: {errors}");
                }

                await userManager.AddToRoleAsync(user, "User");
            }
            else if (string.IsNullOrEmpty(user.ExternalId))
            {

                user.ExternalId = googleUser.Id;
                user.Provider = "Google";
                user.IsActived = true;
                await userManager.UpdateAsync(user);
            }

            var jwtResponse = await authentication.GenerateJWToken(user);

            return Success(jwtResponse);
        }
    }
}
