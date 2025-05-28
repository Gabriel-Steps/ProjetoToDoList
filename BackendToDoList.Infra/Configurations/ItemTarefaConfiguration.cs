using BackendToDoList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendToDoList.Infra.Configurations
{
    public class ItemTarefaConfiguration : IEntityTypeConfiguration<ItemTarefa>
    {
        public void Configure(EntityTypeBuilder<ItemTarefa> builder)
        {
            builder.ToTable("ItensTarefas")
                .HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasMaxLength(260)
                .IsRequired(true);
        }
    }
}
