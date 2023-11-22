using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RedBrowTest.Core.Application.Contracts.Hashers;
using RedBrowTest.Core.Application.Contracts.Identity;
using RedBrowTest.Core.Application.Models.Identity;
using RedBrowTest.Infrastructure.Identity.Services.Authentication;
using RedBrowTest.Infrastructure.Identity.Services.Authenticators;
using RedBrowTest.Infrastructure.Identity.Services.PasswordHashers;
using RedBrowTest.Infrastructure.Identity.Services.TokenGenerators;
using RedBrowTest.Infrastructure.Identity.Services.TokenValidators;

namespace RedBrowTest.Infrastructure.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
            configuration.Bind("JwtSettings", authenticationConfiguration);
            services.AddSingleton(authenticationConfiguration);

            services.AddSingleton<AccessTokenGenerator>();            
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();                        
            //services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
