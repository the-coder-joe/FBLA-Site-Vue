namespace FBLA_Site.Server.Models
{
    public class NewUserRequest
    {
        public string Email { get; set; }
        public string PartiallyHashedPassword { get; set; }
        public string Role { get; set; }
    }
}
