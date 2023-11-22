using RedBrowTest.Core.Application.Constants;
using RedBrowTest.Core.Application.Models.Identity;
using RedBrowTest.Core.Domain;
using System.Security.Claims;

namespace RedBrowTest.Infrastructure.Identity.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(CustomClaimTypes.IdUser, usuario.IdUsuario),
                //new Claim(CustomClaimTypes.ClaimTenantId, usuario.IdEmpresa),
                //new Claim(ClaimTypes.Role, usuario.IdPerfil),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
            };

            return _tokenGenerator.GenerateToken(_configuration.AccessTokenSecret, _configuration.Issuer, _configuration.Audience, _configuration.AccessTokenExpirationMinutes, claims);
        }
    }
}
