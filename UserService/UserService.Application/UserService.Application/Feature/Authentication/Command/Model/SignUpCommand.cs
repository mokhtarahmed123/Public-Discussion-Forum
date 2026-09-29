using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record SignUpCommand(string FullName, string Email, string Password,
      string Phone, string ConfirmPassword
    ) :
      IRequest<Response<string>>;
}
