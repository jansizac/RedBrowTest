using RedBrowTest.Core.Application.Models.Pagination;

namespace RedBrowTest.Core.Application.Features.Usuario.GetAll
{
    public class GetAllResponse
    {
        public List<UsuarioResponse>? Registros { get; set; }
        public GenericPaginationResponse? Meta { get; set; }
    }

    public class UsuarioResponse
    {
        public string IdUsuario { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Edad { get; set; }
    }
}
