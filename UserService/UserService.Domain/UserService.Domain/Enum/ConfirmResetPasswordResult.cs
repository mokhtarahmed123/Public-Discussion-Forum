namespace UserService.Infrastructure.dtos
{
    public enum ConfirmResetPasswordResult
    {
        Success,
        UserNotFound,
        CodeIsWrong,
        InvalidInput,
        ErrorInUpdating,
        FailedToSendEmail
    }
}
