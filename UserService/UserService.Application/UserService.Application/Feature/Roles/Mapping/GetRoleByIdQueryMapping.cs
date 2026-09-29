using UserService.Application.Feature.Roles.Query.Result;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Roles.Mapping
{
    public partial class RoleProfile
    {
        private void GetByIdQueryMapping()
        {
            CreateMap<Role, GetRoleByIdResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
