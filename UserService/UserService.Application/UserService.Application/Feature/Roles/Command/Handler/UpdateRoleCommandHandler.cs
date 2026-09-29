using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Command.Model;

namespace UserService.Application.Feature.Roles.Command.Handler
{
    public class UpdateRoleCommandHandler : ResponseHandler, IRequestHandler<UpdateRoleCommand, Response<string>>
    {

        private readonly IRoleService roleService;


        public UpdateRoleCommandHandler(IRoleService roleService)
        {

            this.roleService = roleService;

        }

        public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await roleService.GetRoleByIdAsync(request.roleId);

                if (role is null)
                {
                    return NotFound<string>("Role not found.");
                }
                role.Name = request.Name;
                role.UpdatedAt = DateTime.UtcNow;
                var isUpdated = await roleService.UpdateRoleAsync(request.roleId, role);

                if (!isUpdated)
                {
                    return BadRequest<string>("Failed to update role.");
                }

                return Success("Role updated successfully.", role.Id.ToString());
            }
            catch (Exception ex)
            {

                return BadRequest<string>(
                    "An error occurred while updating the role."
                    );
            }
        }
    }

}
