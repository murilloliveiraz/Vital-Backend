using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    internal class ExameMap : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.ToTable("exames")
            .HasKey(ex => ex.ExameId);

            builder.Property(ex => ex.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.Local)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.Data)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.HasOne(ex => ex.Paciente)
                .WithMany(p => p.Exames)
                .HasForeignKey(e => e.PacienteId);

            builder.Property(ex => ex.EmailParaReceberResultado)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.ArquivoResultadoUrl)
            .HasColumnType("VARCHAR");

            builder.Property(ex => ex.CaminhoDoS3Local)
            .HasColumnType("VARCHAR");

            builder.Property(ex => ex.PrefixoDaPasta)
            .HasColumnType("VARCHAR");
        }
    }
}
