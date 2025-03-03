namespace AuthenticationService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
    }
}
