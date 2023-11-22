namespace RedBrowTest.Core.Application.Features.Usuario.Delete
{
    public class DeleteResponse
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
