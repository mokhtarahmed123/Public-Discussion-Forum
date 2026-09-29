using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Bases;
using UserService.Application.Feature.Authentication.Query.Model;
using UserService.Domain.Entities;
using UserService.Domain.Helper;
using UserService.Infrastructure.dtos;

namespace UserService.Application.Feature.Authentication.Query.Handler
{
    public class AuthQueryHandler : ResponseHandler,
          IRequestHandler<ConfirmResetPasswordQuery, Response<string>>,
          IRequestHandler<MyProfileQuery, Response<MyProfileQueryResult>>
    {
        private readonly IMapper mapper;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IAuthenticationService authentication;
        private readonly ICurrentUserService currentUserService;

        public AuthQueryHandler(IMapper mapper, UserManager<Users> userManager, RoleManager<Role> roleManager, IAuthenticationService authentication, ICurrentUserService currentUserService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authentication = authentication;
            this.currentUserService = currentUserService;
        }
        public async Task<Response<string>> Handle(
    ConfirmResetPasswordQuery request,
    CancellationToken cancellationToken)
        {
            var result = await authentication.ConfirmResetPassword(
                request.Code,
                request.Email);

            return result switch
            {
                ConfirmResetPasswordResult.UserNotFound =>
                    NotFound<string>("User not found."),

                ConfirmResetPasswordResult.ErrorInUpdating =>
                    BadRequest<string>("Failed to update password."),

                ConfirmResetPasswordResult.FailedToSendEmail =>
                    BadRequest<string>("Failed to send email."),

                ConfirmResetPasswordResult.CodeIsWrong =>
                    BadRequest<string>("Code is wrong."),

                ConfirmResetPasswordResult.InvalidInput =>
                    BadRequest<string>("Invalid email or code."),

                ConfirmResetPasswordResult.Success =>
                    Success<string>("Correct code."),

                _ =>
                    BadRequest<string>("Failed to confirm reset password.")
            };
        }

        public async Task<Response<MyProfileQueryResult>> Handle(MyProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;
            if (userId == null)
            {
                return NotFound<MyProfileQueryResult>("User not found.");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<MyProfileQueryResult>("User not found.");
            }
            var result = new MyProfileQueryResult(
                Id: user.Id,
                FullName: user.FullName,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber
            );
            return Success(result);
        }
    }
}
