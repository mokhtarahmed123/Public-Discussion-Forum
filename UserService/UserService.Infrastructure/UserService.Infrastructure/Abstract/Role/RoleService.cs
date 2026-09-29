using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Feature.Roles;
using UserService.Domain.Entities;
using UserService.Domain.Exception;

namespace UserService.Infrastructure.Abstract.Role
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<UserService.Domain.Entities.Role> roleManager;


        public RoleService(UserManager<Users> userManager, RoleManager<UserService.Domain.Entities.Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

        }
        public async Task<bool> CreateRoleAsync(UserService.Domain.Entities.Role role)
        {
            var Result = await roleManager.CreateAsync(role);
            if (Result.Succeeded) return true;
            return false;

        }

        public async Task<bool> DeleteRoleAsync(Guid Id)
        {
            var role = await roleManager.FindByIdAsync(Id.ToString());
            if (role is null)
            {
                return false;
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return true;
            return false;

        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            return role == null ? false : true;

        }

        public async Task<IEnumerable<UserService.Domain.Entities.Role>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<UserService.Domain.Entities.Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await roleManager.FindByIdAsync(roleId.ToString());

        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(Guid userId)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new NotFoundException("User not found");
            var roles = await userManager.GetRolesAsync(user);
            return roles;

        }

        public async Task<bool> RoleExistsAsync(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            return (true);

        }

        public async Task<bool> UpdateRoleAsync(Guid Id, UserService.Domain.Entities.Role role)
        {
            var existingRole = await roleManager.FindByIdAsync(Id.ToString());
            if (existingRole == null)
            {
                throw new NotFoundException("Role not found");
            }
            existingRole.Name = role.Name;
            var result = await roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
                return true;

            return false;
        }
    }
}
