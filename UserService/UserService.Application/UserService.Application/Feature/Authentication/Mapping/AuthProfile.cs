using AutoMapper;

namespace UserService.Application.Feature.Authentication.Mapping
{
    public partial class AuthProfile : Profile
    {
        public AuthProfile()
        {
            SignUpCommandMapping();
        }
    }
}
