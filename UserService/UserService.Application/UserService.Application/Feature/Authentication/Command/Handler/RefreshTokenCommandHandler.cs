using MediatR;
using UserService.Application.Bases;
using UserService.Application.dtos;
using UserService.Application.Feature.Authentication.Command.Model;

namespace UserService.Application.Feature.Authentication.Command.Handler
{
    public class RefreshTokenCommandHandler : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<JWTAuthResponse>>

    {
        private readonly IAuthenticationService authentication;

        public RefreshTokenCommandHandler(IAuthenticationService authentication)
        {
            this.authentication = authentication;
        }
        public async Task<Response<JWTAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var Result = await authentication.GetRefreshToken(request.RefreshToken, request.Token);
            return Success<JWTAuthResponse>(Result);
        }
    }
}
