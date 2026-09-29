using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authorization.Query.Model;
using UserService.Domain.Entities;
using UserService.Infrastructure.Abstract.Authorization;

namespace UserService.Application.Feature.Authorization.Query.Handler
{
    public class GetUserRolesQueryHandler : ResponseHandler, IRequestHandler<GetUserRolesQuery, Response<IList<string>>>
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IAuthorizationService authorizationService;

        public GetUserRolesQueryHandler(UserManager<Users> userManager, RoleManager<Role> roleManager, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authorizationService = authorizationService;

        }

        public async Task<Response<IList<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await authorizationService.GetUserRolesAsync(request.UserId);

            if (roles is null)
                return NotFound<IList<string>>("User not found.");

            return Success<IList<string>>(roles);
        }
    }
}
