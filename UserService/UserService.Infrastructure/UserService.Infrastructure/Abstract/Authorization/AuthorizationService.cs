using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Email;

namespace UserService.Infrastructure.Abstract.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<UserService.Domain.Entities.Role> roleManager;
        private readonly IEmailService emailService;
        private readonly AppDbContext appDbContext;

        public AuthorizationService(UserManager<Users> userManager,
            RoleManager<UserService.Domain.Entities.Role> roleManager,
            IEmailService emailService,
            AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.emailService = emailService;
            this.appDbContext = appDbContext;
        }
        public async Task<IdentityResult> AssignRoleToUserAsync(Guid RoleId, Guid UserId)
        {
            var user = await userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
                return (IdentityResult.Failed());

            var role = await roleManager.FindByIdAsync(RoleId.ToString());
            if (role == null) return (IdentityResult.Failed());

            if (string.IsNullOrWhiteSpace(role.Name))
            {
                return IdentityResult.Failed();
            }
            var result = await userManager.AddToRoleAsync(
                user,
                role.Name);
            return result;
        }


        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return new List<string>();

            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            return await userManager.IsInRoleAsync(
                user,
                roleName);
        }

        public async Task<IdentityResult> RemoveRoleFromUserAsync(Guid roleId, Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return (IdentityResult.Failed());

            var role = await roleManager.FindByIdAsync(roleId.ToString());

            if (role == null) return (IdentityResult.Failed());

            if (role.Name == null)
                return (IdentityResult.Failed());

            var result = await userManager.RemoveFromRoleAsync(
                user,
                role.Name);

            if (!result.Succeeded)
            {
                return result;
            }

            var addDefaultRoleResult =
                await userManager.AddToRoleAsync(user, "User");

            return addDefaultRoleResult;
        }
    }
}
