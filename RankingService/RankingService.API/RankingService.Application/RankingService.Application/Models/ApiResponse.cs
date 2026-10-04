namespace RankingService.Infrastructure.dtos
{
    public record ApiResponse<T>(bool Succeeded, int StatusCode, string? Message, T? Data);
}
