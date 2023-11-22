using MediatR;

namespace RedBrowTest.Core.Application.Features.Usuario.Create
{
    public class CreateCommand : IRequest<CreateResponse>
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Edad { get; set; }
    }
}
