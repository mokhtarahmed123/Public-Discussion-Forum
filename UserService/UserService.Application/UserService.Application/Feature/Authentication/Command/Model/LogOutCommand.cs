using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Command.Model
{
    public record LogOutCommand : IRequest<Response<string>>;

}
