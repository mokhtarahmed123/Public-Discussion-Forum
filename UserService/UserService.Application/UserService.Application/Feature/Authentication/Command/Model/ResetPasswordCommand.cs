using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record ResetPasswordCommand(string Email, string Password, string ConfirmPassword) : IRequest<Response<string>>;

}
