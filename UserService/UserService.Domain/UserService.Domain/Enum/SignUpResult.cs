namespace UserService.Infrastructure.dtos
{
    public enum SignUpResult
    {
        Success,
        InvalidInput,
        UserWithEmailAlreadyExists,
        UserCreationFailed,
        DefaultRoleNotFound,
        RoleAssignmentFailed,
        HttpContextNotAvailable,
        FailedToSendEmail,
        Failed
    }
}
