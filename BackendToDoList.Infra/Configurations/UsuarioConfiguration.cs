using BackendToDoList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToDoList.Infra.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios")
                .HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired(true);
            builder.HasIndex(x => x.Email)
                .IsUnique(true);

            builder.Property(x => x.Senha)
                .HasMaxLength(50)
                .IsRequired(true);
        }
    }
}
