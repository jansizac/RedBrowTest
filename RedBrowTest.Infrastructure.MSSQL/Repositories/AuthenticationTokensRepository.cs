using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Domain;
using RedBrowTest.Infrastructure.MSSQL.Persistence;

namespace RedBrowTest.Infrastructure.MSSQL.Repositories
{
    public class AuthenticationTokensRepository : RepositoryBase<AuthenticationTokens>, IAuthenticationTokensRepository
    {
        public AuthenticationTokensRepository(RedBrowTestContext context) : base(context)
        {
        }
    }
}
