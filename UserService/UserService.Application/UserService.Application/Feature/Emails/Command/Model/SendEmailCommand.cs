using MediatR;
using UserService.Application.Bases;

namespace UserService.Application.Feature.Emails.Command.Model
{
    public record SendEmailCommand(string Email, string Massege) : IRequest<Response<string>>;

}
