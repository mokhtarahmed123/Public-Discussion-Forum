namespace UserService.Application.Feature.Roles
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(UserService.Domain.Entities.Role role);
        Task<bool> DeleteRoleAsync(Guid Id);
        Task<bool> UpdateRoleAsync(Guid Id, UserService.Domain.Entities.Role role);
        Task<UserService.Domain.Entities.Role?> GetRoleByIdAsync(Guid roleId);
        Task<bool> RoleExistsAsync(Guid roleId);
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);
        Task<IEnumerable<UserService.Domain.Entities.Role>> GetAllRolesAsync();
    }
}
