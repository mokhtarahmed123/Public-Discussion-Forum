using FluentValidation;
using UserService.Application.Feature.Authorization.Command.Model;

namespace UserService.Application.Feature.Authorization.Command.Validator
{
    public class RemoveRoleFromUserCommandValidator : AbstractValidator<RemoveRoleFromUserCommand>
    {
        public RemoveRoleFromUserCommandValidator()
        {
            RuleFor(x => x.userId)
    .NotEmpty().WithMessage("UserId is required.")
    .NotNull().WithMessage("UserId cannot be null.");
            RuleFor(x => x.roleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .NotNull().WithMessage("RoleId cannot be null.");

        }
    }
}
