using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Command.Model;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Roles.Command.Handler
{
    public class CreateRoleCommandHandler : ResponseHandler, IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly IRoleService roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = new Role();
                role.CreatedAt = DateTime.UtcNow;
                role.Name = request.Name;
                var result = await roleService.CreateRoleAsync(role);

                if (!result)
                {
                    return BadRequest<string>(" Failed to Add Role.");
                }
                return Success<string>("Role Added Successfully.", role.Id.ToString());

            }
            catch (Exception ex)
            {
                return ServerError<string>($"An error occurred while processing your request. ,{ex.Message}");
            }
        }
    }
}
