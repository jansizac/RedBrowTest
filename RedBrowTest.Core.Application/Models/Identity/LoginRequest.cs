namespace RedBrowTest.Core.Application.Models.Identity
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty!;
        public string Password { get; set; } = string.Empty!;
    }
}
