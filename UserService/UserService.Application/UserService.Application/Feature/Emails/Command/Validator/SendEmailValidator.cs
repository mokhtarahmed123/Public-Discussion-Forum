using FluentValidation;
using UserService.Application.Feature.Emails.Command.Model;

namespace UserService.Application.Feature.Emails.Command.Validator
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .WithMessage("Email is required.")
                 .EmailAddress()
                 .WithMessage("Invalid email address.");

            RuleFor(x => x.Massege)
                .NotEmpty()
                .WithMessage("Message is required.");
        }
    }
}
