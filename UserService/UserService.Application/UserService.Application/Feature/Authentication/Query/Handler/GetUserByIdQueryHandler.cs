using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authentication.Query.Model;
using UserService.Domain.Entities;

namespace UserService.Application.Feature.Authentication.Query.Handler
{
    public class GetUserByIdQueryHandler : ResponseHandler, IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private readonly UserManager<Users> userManager;

        public GetUserByIdQueryHandler(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<UserDto>("User not found");
            }
            var userDto = new UserDto(user.Id, user.UserName, user.Email);

            return Success(userDto, "User retrieved successfully");

        }
    }
}
