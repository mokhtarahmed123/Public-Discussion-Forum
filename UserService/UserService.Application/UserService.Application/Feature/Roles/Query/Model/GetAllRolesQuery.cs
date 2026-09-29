using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Query.Result;

namespace UserService.Application.Feature.Roles.Query.Model
{
    public record GetAllRolesQuery() : IRequest<Response<List<GetAllRolesResult>>>
 ;
}
