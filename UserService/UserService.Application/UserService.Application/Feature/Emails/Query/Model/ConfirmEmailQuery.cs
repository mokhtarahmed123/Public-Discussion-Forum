using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Emails.Query.Model
{
    public record ConfirmEmailQuery(string Code, Guid UserId) : IRequest<Response<string>>;

}
