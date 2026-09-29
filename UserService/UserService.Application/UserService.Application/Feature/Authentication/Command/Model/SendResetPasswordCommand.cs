using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record SendResetPasswordCommand(string Email) : IRequest<Response<string>>;

}
