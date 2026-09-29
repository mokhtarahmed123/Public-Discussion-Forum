using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Authentication.Query.Model
{

    public record ConfirmResetPasswordQuery(string Code, string Email) : IRequest<Response<string>>;


}
