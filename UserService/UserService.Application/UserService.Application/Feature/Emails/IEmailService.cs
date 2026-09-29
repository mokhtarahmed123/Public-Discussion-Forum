using UserService.Application.dtos;

namespace UserService.Infrastructure.Email
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(Emaildto emaildto);

    }
}
