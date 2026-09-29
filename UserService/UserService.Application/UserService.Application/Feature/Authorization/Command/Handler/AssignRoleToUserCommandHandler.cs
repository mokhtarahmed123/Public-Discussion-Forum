using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authorization.Command.Model;
using UserService.Domain.Entities;
using UserService.Infrastructure.Abstract.Authorization;

namespace UserService.Application.Feature.Authorization.Command.Handler
{
    public class AssignRoleToUserCommandHandler : ResponseHandler, IRequestHandler<AssignRoleToUserCommand, Response<string>>
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IAuthorizationService authorizationService;

        public AssignRoleToUserCommandHandler(UserManager<Users> userManager, RoleManager<Role> roleManager, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authorizationService = authorizationService;
        }
        public async Task<Response<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return NotFound<string>("User not found");
            }

            var role = await roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                return NotFound<string>("Role not found");
            }
            var result = await authorizationService.AssignRoleToUserAsync(role.Id, user.Id);
            if (!result.Succeeded)
            {
                return BadRequest<string>("Failed to assign role");
            }

            return Success<string>("Role assigned successfully");
        }
    }
}
