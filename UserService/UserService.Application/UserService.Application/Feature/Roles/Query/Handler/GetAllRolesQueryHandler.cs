using AutoMapper;
using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Query.Model;
using UserService.Application.Feature.Roles.Query.Result;

namespace UserService.Application.Feature.Roles.Query.Handler
{
    public class GetAllRolesQueryHandler : ResponseHandler, IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesResult>>>

    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public GetAllRolesQueryHandler(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }
        public async Task<Response<List<GetAllRolesResult>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await roleService.GetAllRolesAsync();

            var result = mapper.Map<List<GetAllRolesResult>>(roles);

            return Success(result);
        }
    }
}
