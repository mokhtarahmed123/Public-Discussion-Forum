using MediatR;
using UserService.Application.Bases;
using UserService.Application.dtos;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record LoginCommand(string Email, string Password) : IRequest<Response<JWTAuthResponse>>;


}
