using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Roles.Command.Model
{
    public record DeleteRoleCommand(Guid Id) : IRequest<Response<string>>
    ;
}
