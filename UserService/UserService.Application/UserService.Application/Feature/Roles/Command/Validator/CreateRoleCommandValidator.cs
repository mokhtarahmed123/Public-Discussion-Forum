using FluentValidation;
using UserService.Application.Feature.Roles.Command.Model;

namespace UserService.Application.Feature.Roles.Command.Validator
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {

        private readonly IRoleService roleService;

        public CreateRoleCommandValidator(IRoleService roleService)
        {
            this.roleService = roleService;
            ValidateRoleName();
            ValidateRoleUniqueness();
        }
        private void ValidateRoleName()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");
        }
        private void ValidateRoleUniqueness()
        {

            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellationToken) =>
                    !await roleService.ExistsByNameAsync(name))
                .WithMessage("Role name must be unique.");
        }
    }
}
