namespace FBLA_Site.Server.Models
{
    public class AuthenticationRequest
    {
        public string Username { get; set }
        public string PartiallyHashedPassword { get; set; }
    }
}
