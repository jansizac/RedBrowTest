using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Infrastructure.MSSQL.Persistence;
using RedBrowTest.Infrastructure.MSSQL.Repositories;

namespace RedBrowTest.Infrastructure.MSSQL
{
    public static class InfrastructureMSSQLServiceRegistration
    {
        public static IServiceCollection AddInfrastructureMSSQLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RedBrowTestContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RedBrowConnectionString")), ServiceLifetime.Scoped, ServiceLifetime.Scoped
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IAuthenticationTokensRepository, AuthenticationTokensRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            return services;
        }
    }
}
