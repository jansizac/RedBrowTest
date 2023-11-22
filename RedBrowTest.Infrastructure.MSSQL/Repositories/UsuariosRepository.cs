using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Domain;
using RedBrowTest.Infrastructure.MSSQL.Persistence;

namespace RedBrowTest.Infrastructure.MSSQL.Repositories
{
    public class UsuariosRepository : RepositoryBase<Usuario>, IUsuariosRepository
    {
        public UsuariosRepository(RedBrowTestContext context) : base(context)
        {
        }
    }
}
