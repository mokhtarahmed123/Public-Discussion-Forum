using FluentValidation;
using UserService.Application.Feature.Authentication.Query.Model;

namespace UserService.Application.Feature.Authentication.Query.Validator
{
    public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        public ConfirmResetPasswordQueryValidator()
        {
            RuleFor(x => x.Code)
                    .NotEmpty()
                    .WithMessage("Reset password code is required.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.");
        }
    }
}
