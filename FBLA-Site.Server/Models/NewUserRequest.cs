namespace FBLA_Site
{
    public class NewUserRequest
    {
        public string Email { get; set; }
        public string PartiallyHashedPassword { get; set; }
        public string Role { get; set; }
    }
}
