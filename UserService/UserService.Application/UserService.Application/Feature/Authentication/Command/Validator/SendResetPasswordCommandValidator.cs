using FluentValidation;
using UserService.Application.Feature.Authentication.Command.Model;

namespace UserService.Application.Feature.Authentication.Command.Validator
{
    public class SendResetPasswordCommandValidator : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.");
        }
    }
}
