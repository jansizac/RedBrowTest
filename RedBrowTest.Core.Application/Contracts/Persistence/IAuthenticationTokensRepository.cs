using RedBrowTest.Core.Domain;

namespace RedBrowTest.Core.Application.Contracts.Persistence
{
    public interface IAuthenticationTokensRepository : IAsyncRepository<AuthenticationTokens>
    {
    }
}
