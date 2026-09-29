using MediatR;
using UserService.Application.Bases;
using UserService.Application.dtos;
using UserService.Application.Feature.Emails.Command.Model;
using UserService.Infrastructure.Email;

namespace UserService.Application.Feature.Emails.Command.Handler
{
    public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailService emailService;

        public EmailCommandHandler(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var EmailDto = new Emaildto(request.Email, request.Massege, null);
            var response = await emailService.SendEmailAsync(EmailDto);
            if (response == "Success")
                return Success<string>("Email sent successfully");
            return BadRequest<string>("Failed to send email");
        }
    }
}
