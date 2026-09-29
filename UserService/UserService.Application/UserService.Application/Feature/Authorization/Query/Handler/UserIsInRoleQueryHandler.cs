using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authorization.Query.Model;
using UserService.Domain.Entities;
using UserService.Infrastructure.Abstract.Authorization;

namespace UserService.Application.Feature.Authorization.Query.Handler
{
    public class UserIsInRoleQueryHandler : ResponseHandler,
        IRequestHandler<UserIsInRoleQuery, Response<bool>>
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IAuthorizationService authorizationService;

        public UserIsInRoleQueryHandler(UserManager<Users> userManager, RoleManager<Role> roleManager, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authorizationService = authorizationService;
        }
        public async Task<Response<bool>> Handle(UserIsInRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return NotFound<bool>("User not found");
            }

            var role = await roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                return NotFound<bool>("Role not found");
            }
            var isInRole = await authorizationService.IsInRoleAsync(request.UserId, role.Name);
            if (isInRole)
            {
                return Success(true, $"User is in role {role.Name}");
            }
            else
            {
                return Success(false, $"User is not in role `{role.Name}`");
            }
        }
    }
}
