namespace FBLA_Site.Server.Models
{
    public class User
    {
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Hash { get; set; }
    }
}
