using RedBrowTest.Core.Application.Models.Identity;

namespace RedBrowTest.Core.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserResponse?> Login(LoginRequest loginRequest);
        Task<AuthenticatedUserResponse?> Refresh(RefreshRequest refreshRequest);
        Task Logout(string idUser);
    }
}
