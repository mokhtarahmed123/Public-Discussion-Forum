using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authorization.Command.Model
{
    public record AssignRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<Response<string>>;
}
