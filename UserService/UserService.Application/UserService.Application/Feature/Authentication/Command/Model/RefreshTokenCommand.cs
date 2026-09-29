using MediatR;
using UserService.Application.Bases;
using UserService.Application.dtos;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record RefreshTokenCommand(string RefreshToken, string Token) : IRequest<Response<JWTAuthResponse>>;

}
