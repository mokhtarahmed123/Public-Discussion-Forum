using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Query.Model
{
    public record GetUserByIdQuery(
         Guid Id
     ) : IRequest<Response<UserDto>>
   ;
}


public record UserDto(
    Guid Id,
    string UserName,
    string Email
        );
