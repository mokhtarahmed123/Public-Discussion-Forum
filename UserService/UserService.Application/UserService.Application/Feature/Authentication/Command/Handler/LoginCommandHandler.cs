using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.dtos;
using UserService.Application.Feature.Authentication.Command.Model;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Authentication.Command.Handler
{
    public class LoginCommandHandler : ResponseHandler, IRequestHandler<LoginCommand, Response<JWTAuthResponse>>
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IAuthenticationService authentication;

        public LoginCommandHandler(UserManager<Users> userManager, SignInManager<Users> signInManager, IAuthenticationService authentication)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authentication = authentication;
        }
        public async Task<Response<JWTAuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null) return NotFound<JWTAuthResponse>(" User With Email " + request.Email + " Not Found ");

            var Password = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!user.EmailConfirmed)
                return BadRequest<JWTAuthResponse>();

            if (!Password.Succeeded) return BadRequest<JWTAuthResponse>("Password is incorrect.");
            var Result = await authentication.GenerateJWToken(user);
            return Success<JWTAuthResponse>(Result);
        }
    }
}
