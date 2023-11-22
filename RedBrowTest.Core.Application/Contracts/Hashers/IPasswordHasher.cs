namespace RedBrowTest.Core.Application.Contracts.Hashers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
