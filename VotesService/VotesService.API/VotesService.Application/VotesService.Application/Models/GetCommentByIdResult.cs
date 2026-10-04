namespace VotesService.Application.dtos
{
    public record GetCommentByIdResult(string Id,
        string PostId,
        Guid UserId,
            bool IsDeleted);
}
