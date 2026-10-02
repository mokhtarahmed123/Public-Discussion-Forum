namespace VotesService.Application.dtos
{

    public record GetPostByIdResult(string Id,
        Guid UserId,
        bool IsDeleted,
        bool IsLocked);
    public record ApiResponse<T>(bool Succeeded, int StatusCode, string? Message, T? Data);

}
