using AutoMapper;

namespace UserService.Application.Feature.Roles.Mapping
{
    public partial class RoleProfile : Profile
    {
        public RoleProfile()
        {
            GetByIdQueryMapping();
            GetAllRoleQueryMapping();
        }
    }
}
