using AutoMapper;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Infrastructure.MSSQL.Persistence;
using System.Collections;

namespace RedBrowTest.Infrastructure.MSSQL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable? repositories;
        private readonly RedBrowTestContext _context;
        private readonly IMapper mapper;


        private IUsuariosRepository usuariosRepository;
        public IUsuariosRepository UsuariosRepository => usuariosRepository ??= new UsuariosRepository(_context);


        private IAuthenticationTokensRepository authenticationTokensRepository;
        public IAuthenticationTokensRepository AuthenticationTokensRepository => authenticationTokensRepository ??= new AuthenticationTokensRepository(_context);

        public UnitOfWork(RedBrowTestContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        IAsyncRepository<T> IUnitOfWork.Repository<T>()
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(IAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<T>)repositories[type];
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
