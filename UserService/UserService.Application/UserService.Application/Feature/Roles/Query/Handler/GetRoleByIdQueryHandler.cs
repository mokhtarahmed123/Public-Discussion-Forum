using AutoMapper;
using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Roles.Query.Model;
using UserService.Application.Feature.Roles.Query.Result;

namespace UserService.Application.Feature.Roles.Query.Handler
{
    public class GetRoleByIdQueryHandler : ResponseHandler, IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }
        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await roleService.GetRoleByIdAsync(request.Id);

            if (role is null)
            {
                return NotFound<GetRoleByIdResult>("Role not found.");
            }

            var result = mapper.Map<GetRoleByIdResult>(role);

            return Success(result);
        }
    }
}
