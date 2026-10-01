namespace UserService.Application.Feature.Google.Command.Model
{
    public class GoogleUserModel
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public string? PictureUrl { get; set; }
        public bool EmailVerified { get; set; }
    }
}
