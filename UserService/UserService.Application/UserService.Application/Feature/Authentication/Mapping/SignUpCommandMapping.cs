using UserService.Application.Feature.Authentication.Command.Model;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Authentication.Mapping
{
    public partial class AuthProfile
    {
        private void SignUpCommandMapping()
        {
            CreateMap<SignUpCommand, Users>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
    ;
        }
    }
}
