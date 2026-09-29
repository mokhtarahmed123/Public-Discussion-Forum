using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authorization.Command.Model
{
    public record RemoveRoleFromUserCommand(Guid roleId, Guid userId) : IRequest<Response<string>>
   ;
}
