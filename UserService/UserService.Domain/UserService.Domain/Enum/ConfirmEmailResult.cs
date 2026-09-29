namespace UserService.Infrastructure.dtos
{
    public enum ConfirmEmailResult
    {
        Confirmed,
        UserIdOrCodeNull,
        UserNotFound,
        Failed
    }
}
