using RedBrowTest.Core.Domain.Common;

namespace RedBrowTest.Core.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IAuthenticationTokensRepository AuthenticationTokensRepository { get; }
        IUsuariosRepository UsuariosRepository { get; }

        IAsyncRepository<T> Repository<T>() where T : class, ICommonEntity;

        Task<int> CommitAsync();
    }
}
