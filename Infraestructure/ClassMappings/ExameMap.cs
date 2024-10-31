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

            builder.HasOne(ex => ex.Medico)
                .WithMany(m => m.Exames)
                .HasForeignKey(e => e.MedicoId);

            builder.Property(ex => ex.EmailParaReceberResultado)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.S3KeyPath)
            .HasColumnType("VARCHAR");

            builder.Property(ex => ex.PrefixoDaPasta)
            .HasColumnType("VARCHAR");
        }
    }
}
