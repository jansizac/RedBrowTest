using RedBrowTest.Core.Application.Contracts.Hashers;
using RedBrowTest.Core.Application.Contracts.Identity;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Application.Models.Identity;
using RedBrowTest.Infrastructure.Identity.Services.Authenticators;
using RedBrowTest.Infrastructure.Identity.Services.TokenValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Infrastructure.Identity.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWork unitOfWork;
        private readonly RefreshTokenValidator refreshTokenValidator;
        private readonly Authenticator authenticator;

        public AuthenticationService(IPasswordHasher passwordHasher,
                                     IUnitOfWork unitOfWork,
                                     RefreshTokenValidator refreshTokenValidator,
                                     Authenticator authenticator)
        {
            this.passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
            this.refreshTokenValidator = refreshTokenValidator;
            this.authenticator = authenticator;
        }

        public async Task<AuthenticatedUserResponse?> Login(LoginRequest loginRequest)
        {
            var usuario = await unitOfWork.UsuariosRepository.GetFirstOrDefaultAsync(x => x.Email == loginRequest.Username);

            if (usuario == null)
            {
                return null;
            }

            bool isCorrectPassword = passwordHasher.VerifyPassword(loginRequest.Password, usuario.Password);

            if (!isCorrectPassword)
            {
                return null;
            }

            AuthenticatedUserResponse response = await authenticator.Authenticate(usuario);

            return response;
        }

        public async Task Logout(string idUser)
        {
            var tokens = await unitOfWork.AuthenticationTokensRepository.GetAsync(x => x.IdUsuario == idUser);
            // borramos uno a uno los tokens
            foreach (var token in tokens)
            {
                await unitOfWork.AuthenticationTokensRepository.DeleteAsync(token);
            }
        }

        public async Task<AuthenticatedUserResponse?> Refresh(RefreshRequest refreshRequest)
        {
            bool isValidRefreshToken = refreshTokenValidator.ValidateToken(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return null;
            }
            var refreshToken = await unitOfWork.AuthenticationTokensRepository.GetFirstOrDefaultAsync(x => x.Token == refreshRequest.RefreshToken);
            if (refreshToken == null)
            {
                return null;
            }

            await unitOfWork.AuthenticationTokensRepository.DeleteAsync(refreshToken);

            var usuario = await unitOfWork.UsuariosRepository.GetByIdAsync(refreshToken.IdUsuario);
            if (usuario == null)
            {
                return null;
            }

            AuthenticatedUserResponse response = await authenticator.Authenticate(usuario);

            return response;
        }
    }
}
