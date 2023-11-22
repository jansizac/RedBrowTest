using RedBrowTest.Core.Domain.Common;

namespace RedBrowTest.Core.Domain
{
    public class AuthenticationTokens : ICommonEntity
    {
        public string IdAuthenticationToken { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Token { get; set; } = null!;
        public string IdUsuario { get; set; } = null!;

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
