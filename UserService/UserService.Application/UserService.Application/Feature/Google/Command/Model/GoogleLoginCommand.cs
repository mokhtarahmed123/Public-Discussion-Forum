using MediatR;
using UserService.Application.Bases;
using UserService.Application.dtos;

namespace UserService.Application.Feature.Google.Command.Model
{
    public record GoogleLoginCommand(string IdToken) : IRequest<Response<JWTAuthResponse>>;
    public record GoogleLoginDto
    {
        public string IdToken { get; set; }
    }
}
