namespace UserService.Infrastructure.dtos
{
    public enum ResetPasswordResult
    {
        Success,
        InvalidInput,
        UserNotFound,
        InvalidPassword,
        Failed
    }
}
