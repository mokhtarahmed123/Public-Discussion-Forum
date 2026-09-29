using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Roles.Command.Model
{
    public record CreateRoleCommand(string Name) : IRequest<Response<string>>;
}
