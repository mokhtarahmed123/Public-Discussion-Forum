namespace UserService.Application.dtos
{
    public record Emaildto(
         string Email,
         string Massage,
         string? reason
     );
}
