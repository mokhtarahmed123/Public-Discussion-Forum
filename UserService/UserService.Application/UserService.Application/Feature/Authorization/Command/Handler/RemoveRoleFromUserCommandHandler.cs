using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authorization.Command.Model;
using UserService.Domain.Entities;
using UserService.Infrastructure.Abstract.Authorization;

namespace UserService.Application.Feature.Authorization.Command.Handler
{
    public class RemoveRoleFromUserCommandHandler : ResponseHandler, IRequestHandler<RemoveRoleFromUserCommand, Response<string>>
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IAuthorizationService authorizationService;

        public RemoveRoleFromUserCommandHandler(UserManager<Users> userManager, RoleManager<Role> roleManager, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authorizationService = authorizationService;
        }
        public async Task<Response<string>> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.userId.ToString());

            if (user == null)
            {
                return NotFound<string>("User not found");
            }

            var role = await roleManager.FindByIdAsync(request.roleId.ToString());

            if (role == null)
            {
                return NotFound<string>("Role not found");
            }
            var result = await authorizationService.RemoveRoleFromUserAsync(role.Id, user.Id);
            if (!result.Succeeded)
            {
                return BadRequest<string>("Failed to remove role");
            }
            return Success<string>("Role removed successfully");

        }
    }
}
