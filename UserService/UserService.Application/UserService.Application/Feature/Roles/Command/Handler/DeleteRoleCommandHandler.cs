using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Command.Model;

namespace UserService.Application.Feature.Roles.Command.Handler
{
    public class DeleteRoleCommandHandler : ResponseHandler, IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IRoleService roleService;
        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await roleService.GetRoleByIdAsync(request.Id);

                if (role is null)
                {
                    return NotFound<string>("Role not found.");
                }
                var result = await roleService.DeleteRoleAsync(request.Id);
                if (!result)
                {
                    return BadRequest<string>("Failed to delete role.");
                }
                return Success("Role deleted successfully.", role.Id.ToString());
            }
            catch (Exception ex)
            {

                return BadRequest<string>(
                    "An error occurred while deleting the role."
                   );
            }
        }
    }
}
