namespace UserService.Infrastructure.dtos
{
    public enum SendResetPasswordCodeResult
    {
        Success,
        InvalidInput,
        UserNotFound,
        ErrorInUpdating,
        FailedToSendEmail,
        Failed
    }
}
