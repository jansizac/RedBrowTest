using MediatR;

namespace RedBrowTest.Core.Application.Features.Usuario.Update
{
    public class UpdateCommand : IRequest<UpdateResponse>
    {
        public string IdUsuario { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Edad { get; set; }
    }
}
