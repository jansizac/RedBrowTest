using RedBrowTest.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Infrastructure.MSSQL.Seed
{
    public class DataSeed
    {
        public static List<Usuario> InitialUsuarios()
        {
            return new List<Usuario>()
            {
                new Usuario()
                {
                    IdUsuario = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    Nombre = "admin",
                    Email = "admin@mail.com",
                    Password = "123456",
                    Edad = 100
                }
            };
        }
    }
}
