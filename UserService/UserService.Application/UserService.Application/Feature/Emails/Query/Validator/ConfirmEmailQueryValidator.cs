using FluentValidation;
using UserService.Application.Feature.Emails.Query.Model;

namespace UserService.Application.Feature.Emails.Query.Validator
{
    public class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code is required.");
        }
    }
}
