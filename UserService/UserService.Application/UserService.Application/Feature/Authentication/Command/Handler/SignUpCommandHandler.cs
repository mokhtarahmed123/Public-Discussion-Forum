using AutoMapper;
using MediatR;
using UserService.Application.Bases;
using UserService.Application.Feature.Authentication.Command.Model;
using UserService.Domain.Entities;
using UserService.Infrastructure.dtos;

namespace UserService.Application.Feature.Authentication.Command.Handler
{
    public class SignUpCommandHandler : ResponseHandler, IRequestHandler<SignUpCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly IAuthenticationService authentication;

        public SignUpCommandHandler(IMapper mapper, IAuthenticationService authentication)
        {
            this.mapper = mapper;
            this.authentication = authentication;
        }
        public async Task<Response<string>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Users>(request);
            var result = await authentication.SignUpAsync(user, request.Password);
            return result switch
            {
                SignUpResult.Success => Success<string>("User registered successfully. Please check your email to confirm your account."),
                SignUpResult.InvalidInput => BadRequest<string>("Invalid user information."),
                SignUpResult.UserWithEmailAlreadyExists => BadRequest<string>("Email is already registered."),
                SignUpResult.UserCreationFailed => BadRequest<string>("Failed to create user."),
                SignUpResult.DefaultRoleNotFound => BadRequest<string>("Default role 'User' was not found."),
                SignUpResult.RoleAssignmentFailed => BadRequest<string>("Failed to assign user role."),
                SignUpResult.HttpContextNotAvailable => BadRequest<string>("HTTP context is not available."),
                SignUpResult.FailedToSendEmail => BadRequest<string>("User registration failed because the confirmation email could not be sent."),
                SignUpResult.Failed => BadRequest<string>("Failed to register user."),
                _ => BadRequest<string>("Failed to register user.")
            };
        }
    }
}
