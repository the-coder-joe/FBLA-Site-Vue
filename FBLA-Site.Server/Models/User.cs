namespace FBLA_Site
{
    public class User
    {
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Hash { get; set; }
    }
}
