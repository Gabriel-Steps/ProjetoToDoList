using BackendToDoList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BackendToDoList.Infra.Configurations
{
    public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas")
                .HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired(true);

            builder.Property(x => x.Descricao)
                .HasMaxLength(260)
            .IsRequired(true);

            builder.HasMany(t => t.ItensTarefa)
            .WithOne(it => it.Tarefa)
            .HasForeignKey(it => it.TarefaId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
