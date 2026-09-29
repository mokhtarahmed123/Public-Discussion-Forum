using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Roles.Command.Model
{
    public record UpdateRoleCommand(Guid roleId, string Name) : IRequest<Response<string>>;



}
