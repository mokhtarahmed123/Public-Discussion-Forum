using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Query.Result;

namespace UserService.Application.Feature.Roles.Query.Model
{
    public record GetRoleByIdQuery(Guid Id) : IRequest<Response<GetRoleByIdResult>>;

}
