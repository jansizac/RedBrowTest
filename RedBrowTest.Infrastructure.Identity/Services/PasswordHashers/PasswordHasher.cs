using RedBrowTest.Core.Application.Contracts.Hashers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Infrastructure.Identity.Services.PasswordHashers
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //return BCrypt.Net.BCrypt.HashPassword(password);
            return password;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            //return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return password == passwordHash;
        }
    }
}
