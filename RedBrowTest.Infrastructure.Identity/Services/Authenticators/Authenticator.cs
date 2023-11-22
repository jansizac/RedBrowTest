using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Application.Models.Identity;
using RedBrowTest.Core.Domain;
using RedBrowTest.Infrastructure.Identity.Services.TokenGenerators;

namespace RedBrowTest.Infrastructure.Identity.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IAuthenticationTokensRepository authenticationTokensRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator,
                             RefreshTokenGenerator refreshTokenGenerator,
                             IAuthenticationTokensRepository authenticationTokensRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            this.authenticationTokensRepository = authenticationTokensRepository;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(Usuario usuario)
        {
            string accessToken = _accessTokenGenerator.GenerateToken(usuario);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            AuthenticationTokens authenticationToken = new AuthenticationTokens()
            {
                IdUsuario = usuario.IdUsuario,
                Token = refreshToken,
            };
            authenticationToken = await authenticationTokensRepository.AddAsync(authenticationToken);

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
