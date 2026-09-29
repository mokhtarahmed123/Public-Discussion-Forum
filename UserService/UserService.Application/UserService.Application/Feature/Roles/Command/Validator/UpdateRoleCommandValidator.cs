using FluentValidation;
using UserService.Application.Feature.Roles.Command.Model;

namespace UserService.Application.Feature.Roles.Command.Validator
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.roleId)
                .NotEmpty()
                .WithMessage("Role Id is required.");

            IfRoleIsFoundThenValidateRoleName();
        }

        private void ValidateRoleName()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .MaximumLength(100)
                .WithMessage("Role name must not exceed 100 characters.");
        }

        private void IfRoleIsFoundThenValidateRoleName()
        {
            When(x => !string.IsNullOrWhiteSpace(x.roleId.ToString()), () =>
            {
                ValidateRoleName();
            });
        }
    }
}
