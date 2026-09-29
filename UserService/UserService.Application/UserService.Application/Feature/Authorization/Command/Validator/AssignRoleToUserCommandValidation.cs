using FluentValidation;
using UserService.Application.Feature.Authorization.Command.Model;

namespace UserService.Application.Feature.Authorization.Command.Validator
{
    public class AssignRoleToUserCommandValidation : AbstractValidator<AssignRoleToUserCommand>
    {
        public AssignRoleToUserCommandValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotNull().WithMessage("UserId cannot be null.");
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .NotNull().WithMessage("RoleId cannot be null.");
        }
    }
}
