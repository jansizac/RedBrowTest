using Microsoft.EntityFrameworkCore;
using RedBrowTest.Core.Domain;
using RedBrowTest.Infrastructure.MSSQL.Seed;

namespace RedBrowTest.Infrastructure.MSSQL.Persistence
{
    public class RedBrowTestContext : DbContext
    {
        public RedBrowTestContext(DbContextOptions<RedBrowTestContext> options) : base(options)
        {
                
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<AuthenticationTokens>(entity =>
            {
                entity.HasKey(e => e.IdAuthenticationToken);

                entity.Property(e => e.IdAuthenticationToken).HasMaxLength(36).HasDefaultValueSql("NEWID()");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Token);
                entity.Property(e => e.IdUsuario).HasMaxLength(36);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AuthenticationTokens)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthenticationTokens_Usuarios");
            });

            builder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasMaxLength(36).HasDefaultValueSql("NEWID()");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(320);                
            });

            builder.Entity<Usuario>().HasData(DataSeed.InitialUsuarios());
        }
    }
}
