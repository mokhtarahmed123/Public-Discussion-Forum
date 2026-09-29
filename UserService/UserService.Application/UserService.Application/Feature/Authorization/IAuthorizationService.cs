using Microsoft.AspNetCore.Identity;

namespace UserService.Infrastructure.Abstract.Authorization
{
    public interface IAuthorizationService
    {
        Task<IdentityResult> AssignRoleToUserAsync(Guid RoleId, Guid UserId);
        Task<IdentityResult> RemoveRoleFromUserAsync(Guid RoleId, Guid UserId);
        Task<bool> IsInRoleAsync(
     Guid userId,
     string roleName);
        Task<IList<string>> GetUserRolesAsync(Guid userId);

    }
}
