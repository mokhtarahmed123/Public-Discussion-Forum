using FluentValidation;
using UserService.Application.Feature.Authentication.Command.Model;

namespace UserService.Application.Feature.Authentication.Command.Validator
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                        .NotEmpty()
                        .WithMessage("Email is required.")
                        .EmailAddress()
                        .WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
