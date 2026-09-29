using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authorization.Query.Model
{
    public record GetUserRolesQuery(Guid UserId)
            : IRequest<Response<IList<string>>>;
}
