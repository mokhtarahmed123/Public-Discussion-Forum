namespace VotesService.Application.dtos
{
    public record UserDto(
      Guid Id,
      string? UserName,
      string? Email);
    public record AuthResponse<T>(bool Succeeded, int StatusCode, string? Message, T? Data);

}
