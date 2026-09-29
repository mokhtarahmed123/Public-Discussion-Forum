using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authorization.Query.Model
{
    public record UserIsInRoleQuery(Guid UserId, Guid RoleId) : IRequest<Response<bool>>
;
}
