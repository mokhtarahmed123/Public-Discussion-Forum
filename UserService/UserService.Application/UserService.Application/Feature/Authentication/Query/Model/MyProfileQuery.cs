using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Query.Model
{
    public record MyProfileQuery() : IRequest<Response<MyProfileQueryResult>>
 ;
    public record MyProfileQueryResult(
        Guid Id,
        string FullName,
                string Email,
        string PhoneNumber
    );
}
